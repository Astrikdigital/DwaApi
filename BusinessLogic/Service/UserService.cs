using BusinessLogicLayer.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccessLayer.Interface;
using BusinessObjectsLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Spreadsheet;
using DataAccessLayer.Repositories;
using DocumentFormat.OpenXml.Math;
using ErrorLog;
using static Converge.Shared.Helper.EnumHelper;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Text.Json.Nodes;

namespace BusinessLogicLayer.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly string? _secretKey;
        private readonly string? _issuer;
        private readonly string? _audience;
        private readonly int _durationInMinutes;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _secretKey = configuration["Jwt:SecretKey"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _durationInMinutes = int.Parse(configuration["Jwt:DurationInMinutes"]);
        }

        public async Task<BusinessObjectsLayer.Entities.User> Authenticate(string UserId, string password)
        {
            BusinessObjectsLayer.Entities.User user = await _userRepository.Authenticate(UserId.ToLower(),password);
            if (user == null || user.Password != password) // Password Hashed
            {
                return null;
            }
            if(user.StudentEnrollment != null) user.StudentEnrollment = JsonObject.Parse(user.StudentEnrollment);
            var token = GenerateJwtToken(user);
            return new BusinessObjectsLayer.Entities.User { Token = token, Expiry = DateTime.UtcNow.AddMinutes(_durationInMinutes),Role=user.Role,RoleId=user.RoleId,FullName=user.FullName,Email=user.Email,Id = user.Id,UserTypeId=user.UserTypeId,PictureUrl=user.PictureUrl,EntityId=user.EntityId, UnReadCount =user.UnReadCount,EnrollmentId=user.EnrollmentId,StudentEnrollment=user.StudentEnrollment};
        }

        public async Task<dynamic> ChangePassword(ChangePassword changePassword)
        {
            var user = await _userRepository.ChangePassword(changePassword);
            return user;
        }

        public Task<dynamic> DeleteUser(int Id)
        {
            try
            {
                var res = _userRepository.DeleteUser(Id);
                return res;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(DeleteUser), ErrorLogger.ConvertToDictionary(nameof(Id), Id));
                ErrorLogger.LogError("UserService", "DeleteUser", request, ex);
                return null;
            }
        }

        public async Task<dynamic> GetHelpCenterQuery(int? UserId, int? PageSize, int? PageNubmer = 1)
        {
            try
            {
                var user = await _userRepository.GetHelpCenterQuery(UserId,PageSize,PageNubmer);
                return user;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetHelpCenterQuery), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId,nameof(PageSize),PageSize,nameof(PageNubmer), PageNubmer));
                ErrorLogger.LogError("UserService", "GetHelpCenterQuery", request, ex);
                return null;
            }
        }

        public async Task<dynamic> GetQueryByThreadId(int? ThreadId, int? UserId)
        {
            try
            {
                var user = await _userRepository.GetQueryByThreadId(ThreadId, UserId);
                return user;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetQueryByThreadId), ErrorLogger.ConvertToDictionary(nameof(ThreadId), ThreadId, nameof(UserId), UserId));
                ErrorLogger.LogError("UserService", "GetQueryByThreadId", request, ex);
                return null;
            }
        }

        public async Task<dynamic> GetUserById(int? UserId, int? UserTypeId, int? pagenumber, int? pagesize, string? SearchText = null)
        {
            try
            {
                var res = await _userRepository.GetUserById(UserId,UserTypeId, pagenumber, pagesize, SearchText);
                return res;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetUserById), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId, nameof(pagenumber), pagenumber, nameof(pagesize), pagesize));
                ErrorLogger.LogError("UserService", "GetUserById", request, ex);
                return null;
            }
        }

        public async Task<List<BusinessObjectsLayer.Entities.User>> GetUserId(int UserId, List<int> BatchIds)
        {
            var user = await _userRepository.GetUserId(UserId,BatchIds);
            return user;
        }

        public async Task<dynamic> GetUserType()
        {
            try
            {
                var res = await _userRepository.GetUserType();
                return res;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("UserService", "GetUserType", "", ex);
                return null;
            }
        }

        public async Task<dynamic> InsertUpdateUser(UserDto user)
        {
            try
            {
                var res = await _userRepository.InsertUpdateUser(user);
                return res;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(InsertUpdateUser), ErrorLogger.ConvertObjectToDictionary(user));
                ErrorLogger.LogError("UserService", "InsertUpdateUser", request, ex);
                return null;
            }
        }

        public async Task<List<dynamic>> MyProfile(int? UserId, int? UserTypeId)
        {
            try
            {
                var user = await _userRepository.MyProfile(UserId, UserTypeId);
                return user;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetUserType), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId, nameof(UserTypeId), UserTypeId));
                ErrorLogger.LogError("UserService", "GetUserType", request, ex);
                return null;
            }
        }

        public Task<dynamic> UpdateQueryStatus(int? Id, int? UserId)
        {
            try
            {
                var res = _userRepository.UpdateQueryStatus(Id, UserId);
                return res;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(UpdateQueryStatus), ErrorLogger.ConvertToDictionary(nameof(Id), Id,nameof(UserId), UserId));
                ErrorLogger.LogError("UserService", "UpdateQueryStatus", request, ex);
                return null;
            }
        }

        private string GenerateJwtToken(BusinessObjectsLayer.Entities.User user)
        {
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_durationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
