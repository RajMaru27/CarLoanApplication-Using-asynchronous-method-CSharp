using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;

namespace CoreAPIs.Repository.Interface
{
    public interface IUserRepository
    {
        Task<GeneralModel> AddAsync(UserRequest detail);
        Task<GeneralModel> UpdateAsync(UserRequest detail);
        Task<List<UserActivityDTO>> GetActivity(UserActivityFilter filter, bool count);
        Task<GeneralModel> DeleteUser(UserStatusUpdate model);
        Task<GeneralModel> Restore(UserStatusUpdate model);
        Task<GeneralModel> UpdateFilter(SaveUserFilter model);
        Task<List<UserList>> List();
        Task<UserList> Details(string UserId);
        Task<List<UserList>> SearchList(UserSearchData search);
    }
}
