
using BusinessObjectsLayer.Entities;
using Converge.Shared.Helper;
using Dapper;
using DataAccess.DbContext;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ErrorLog;
using Microsoft.AspNetCore.Http;
using System.Data;
using static Converge.Shared.Helper.EnumHelper;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly HttpContextAccessor _httpContextAccessor;
        public UserRepository(HttpContextAccessor httpContextAccessor,DapperContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BusinessObjectsLayer.Entities.User> Authenticate(string userId,string password)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var user = await connection.QueryFirstOrDefaultAsync<BusinessObjectsLayer.Entities.User>("Login @UserId, @Password", new { UserId = userId, Password = password });
                if (user != null)
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(Authenticate), ErrorLogger.ConvertToDictionary(nameof(userId), userId, nameof(password), password));
                ErrorLogger.LogError("UserRepository", "Authenticate", request, ex);
            }
            return null;
        }

        public async Task<dynamic> ChangePassword(ChangePassword changePassword)
        {
            var resp = new Object();
            try
            {
                var param = new
                {
                    UserId = changePassword.Id,
                    CurrentPassword = changePassword.CurrentPassword,
                    NewPassword = changePassword.NewPassword
                };
                using var connection = _context.CreateConnection();
                resp = await connection.QueryFirstOrDefaultAsync<dynamic>("ChangePassword", param: param);
                return resp;
                
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(ChangePassword), ErrorLogger.ConvertToDictionary(changePassword));
                ErrorLogger.LogError("UserRepository", "ChangePassword", request, ex);
            }
            return null;
        }

        public async Task<dynamic> DeleteUser(int? Id)
        {
            var result = new Result();
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new
                    {
                        Id = Id,
                        UserId = Helper.UserId(_httpContextAccessor)
                    };
                    await connection.ExecuteAsync("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
                    result.IsSuccess = true;
                    result.Message = result.IsSuccess ? "User deleted successfully." : "failed to delete!";
                    result.Status = result.IsSuccess ? "Success" : "Error";
                }

            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(DeleteUser), ErrorLogger.ConvertToDictionary(nameof(Id), Id));
                ErrorLogger.LogError("UserRepository", "DeleteUser", request, ex);
                result.Data = null;
            }
            return result;
        }

        public async Task<dynamic> GetHelpCenterQuery(int? UserId, int? PageSize, int? PageNubmer = 1)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    UserId = UserId,
                    PageSize = PageSize,
                    PageNubmer = PageNubmer
                };
                var res = (await con.QueryAsync<dynamic>("GetHelpCenterQuery", param: parameters, commandType: CommandType.StoredProcedure)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetHelpCenterQuery), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId));
                ErrorLogger.LogError("UserRepository", "GetHelpCenterQuery", request, ex);
                Console.WriteLine(ex.Message);
            }
            return (null);
        }

        public async Task<dynamic> GetQueryByThreadId(int? ThreadId, int? UserId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var user = await connection.QueryAsync<dynamic>("GetQueryByThreadId @Id,@UserId", new { Id = ThreadId, UserId = UserId });
                if (user != null)
                {
                    return user.ToList();
                }
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetQueryByThreadId), ErrorLogger.ConvertToDictionary(nameof(Id), ThreadId, nameof(UserId), UserId));
                ErrorLogger.LogError("UserRepository", "GetQueryByThreadId", request, ex);
            }
            return null;
        }

        public async Task<dynamic> GetUserById(int? UserId, int? UserTypeId,int?PageNumber,int?PageSize, string? SearchText = null)
        {
            try
            {
                using var con = _context.CreateConnection();
                var parameters = new
                {
                    UserId = UserId,
                    UserTypeId= UserTypeId,
                    PageNumber =PageNumber,
                    PageSize=PageSize,
                    SearchText = SearchText
                };
                var res = (await con.QueryAsync<dynamic>("GetUsers", param: parameters, commandType: CommandType.StoredProcedure)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetUserById), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId, nameof(PageNumber), PageNumber, nameof(PageSize), PageSize,nameof(SearchText), SearchText));
                ErrorLogger.LogError("UserRepository", "GetUserById", request, ex);
                Console.WriteLine(ex.Message);
            }
            return (null);
        }

        public async Task<List<BusinessObjectsLayer.Entities.User>> GetUserId(int UserId, List<int> BatchIds)
        {
            try
            {
                string BatchId = BatchIds != null ? "string" : null;
                using var connection = _context.CreateConnection();
                var user = await connection.QueryAsync<BusinessObjectsLayer.Entities.User>("GetUserId @UserId,@BatchIds", new { UserId = UserId, BatchIds = BatchId });
                if (user != null)
                {
                    return user.ToList();
                }
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(GetUserId), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId));//BatchIds to pass
                ErrorLogger.LogError("UserRepository", "GetUserId", request, ex);
            }
            return null;
        }

        public async Task<dynamic> GetUserType()
        {
            try
            {
                using var con = _context.CreateConnection();
                var res = (await con.QueryAsync<dynamic>("GetUserType",new { UserId = Helper.UserId(_httpContextAccessor) }, commandType: CommandType.StoredProcedure)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("UserRepository", "GetUserType", "", ex);
                Console.WriteLine(ex.Message);
            }
            return (null);
        }

        public async Task<dynamic> InsertUpdateUser(UserDto user)
        {
            try
            {
                using (IDbConnection con = _context.CreateConnection())
                {
                    var parameters = new
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        UserTypeId = user.UserTypeId,
                        EntityId = user.EntityId,
                        Email = user.Email,
                        Password = user.Password,
                        UserName = user.UserName,
                        PictureUrl = user.PictueUrl,
                        UserId = Helper.UserId(_httpContextAccessor)
                    };

                    var resp = (await con.QueryAsync<dynamic>("InsertUpdateUser", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                    user.Id = resp.Id;
                    return user;
                }
                return user;
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(InsertUpdateUser), ErrorLogger.ConvertObjectToDictionary(user));
                ErrorLogger.LogError("UserRepository", "InsertUpdateUser", request, ex);
                return null;
            }
        }


        public async Task<List<dynamic>> MyProfile(int? UserId, int? UserTypeId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var user = await connection.QueryAsync<dynamic>("MyProfile @UserId,@UserTypeId", new { UserId = UserId, UserTypeId = UserTypeId });
                if (user != null)
                {
                    return user.ToList();
                }
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(MyProfile), ErrorLogger.ConvertToDictionary(nameof(UserId), UserId,nameof(UserTypeId), UserTypeId));
                ErrorLogger.LogError("UserRepository", "MyProfile", request, ex);
            }
            return null;
        }

        public async Task<dynamic> UpdateQueryStatus(int? Id, int? UserId)
        {
            var result = new Result();
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new
                    {
                        Id = Id,
                        UserId = Helper.UserId(_httpContextAccessor)
                    };
                    await connection.ExecuteAsync("UpdateQueryStatus", parameters, commandType: CommandType.StoredProcedure);
                    result.IsSuccess = true;
                    result.Message = result.IsSuccess ? "User updated successfully." : "failed to delete!";
                    result.Status = result.IsSuccess ? "Success" : "Error";
                }

            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(DeleteUser), ErrorLogger.ConvertToDictionary(nameof(Id), Id,nameof(UserId), UserId));
                ErrorLogger.LogError("UserRepository", "UpdateQueryStatus", request, ex);
                result.Data = null;
            }
            return result;
        }
    }
}
