using BusinessObjectsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string UserId,string UserPassword);
        Task<List<User>> GetUserId(int UserId, List<int> BatchIds);
        Task<List<dynamic>> MyProfile(int? UserId, int? UserTypeId);
        Task<dynamic> ChangePassword(ChangePassword changePassword);
        Task<dynamic> InsertUpdateUser(UserDto user);
        Task<dynamic> GetUserById(int? UserId,int? UserTypeId,int?pagenumber,int?pagesize, string? SearchText = null);
        Task<dynamic> DeleteUser(int Id);
        Task<dynamic> GetUserType();
        Task<dynamic> GetQueryByThreadId(int? ThreadId, int? UserId);
        Task<dynamic> UpdateQueryStatus(int? Id, int? UserId);
        Task<dynamic> GetHelpCenterQuery(int? UserId, int? PageSize, int? PageNubmer = 1);

    }
}
