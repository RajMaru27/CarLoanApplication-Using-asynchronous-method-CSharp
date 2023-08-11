using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;
using System.Data;

namespace CoreAPIs.Repository.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _context;
        public UserRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GeneralModel> AddAsync(UserRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var Userfilter = Builders<Users>.Filter.Where(x => x.UserName == request.UserName || x.Email == request.Email);
                var Userdetail = await _context.Users.Find(Userfilter).FirstOrDefaultAsync();

                if (Userdetail == null)
                {
                    Users users = new Users();

                    users = new Users
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        UserName = request.UserName,
                        Status = true,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _context.Users.InsertOneAsync(users);

                    model = new GeneralModel()
                    {
                        Id = users.Id,
                        Status = true,
                        Message = "User Created"
                    };
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(UserRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var Fliter = Builders<Users>.Filter.Where(x => x.Id == request.UserId);

                var updatebuilder = Builders<Users>.Update;
                var updateDef = updatebuilder.Combine(new UpdateDefinition<Users>[]
                {
                    updatebuilder.Set(x => x.FirstName,request.FirstName),
                    updatebuilder.Set(x => x.LastName,request.LastName),
                    updatebuilder.Set(x => x.Email,request.Email),
                    updatebuilder.Set(x => x.UserName,request.UserName),
                    updatebuilder.Set(x => x.UpdateDate,DateTime.UtcNow),
                });
                await _context.Users.FindOneAndUpdateAsync(Fliter, updateDef);
                model.Id = request.UserId;
                model.Status = true;
                model.Message = "User Updated";
                return model;
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.Find(x => !string.IsNullOrEmpty(x.Email)).ToListAsync();
        }

        public async Task<List<UserActivityDTO>> GetActivity(UserActivityFilter filter, bool count)
        {
            List<UserActivityDTO> Activity = new List<UserActivityDTO>();

            var activityfilter = Builders<Users>.Filter.Where(x => x.Status == (filter.IsActive.Equals("Active", StringComparison.InvariantCultureIgnoreCase)));
            var allactivity = await _context.Users.Find(activityfilter).ToListAsync();

            allactivity.ForEach(x => Activity.Add(new UserActivityDTO
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                Statuss = x.Status ? "Active" : "InActive",
                Email = x.Email,
                CreatedDate = x.CreatedDate,
                UpdateDate = x.UpdateDate,
            }));

            if (!string.IsNullOrEmpty(filter.Email))
            {
                Activity = Activity.Where(x => x.FirstName.Contains(filter.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                Activity = Activity.Where(x => x.FirstName.Contains(filter.UserName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            Activity = Activity.OrderByDescending(x => x.UpdateDate).ToList();
            if (!count)
            {
                Activity = Activity.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
            }
            return Activity;
        }

        public async Task<GeneralModel> DeleteUser(UserStatusUpdate model)
        {
            GeneralModel generalModel = new GeneralModel();
            try
            {
                var filter = Builders<Users>.Filter.Where(x => x.Id == model.UserId);
                var updatefilter = Builders<Users>.Update;
                var updatedef = updatefilter.Combine(new UpdateDefinition<Users>[]
                {
                    updatefilter.Set(x => x.Status, false),
                    updatefilter.Set(x => x.UpdateDate, DateTime.UtcNow)
                });
                await _context.Users.FindOneAndUpdateAsync(filter, updatedef);
                generalModel.Id = model.UserId;
                generalModel.Status = true;
                generalModel.Message = "";
            }
            catch (Exception ex)
            {
                generalModel.Status = false;
                generalModel.Message = ex.Message;
            }
            return generalModel;
        }

        public async Task<GeneralModel> Restore(UserStatusUpdate model)
        {
            GeneralModel generalModel = new GeneralModel();
            try
            {
                var filter = Builders<Users>.Filter.Where(x => x.Id == model.UserId);
                var filterUpdate = Builders<Users>.Update;
                var filterDef = filterUpdate.Combine(new UpdateDefinition<Users>[]
                {
                    filterUpdate.Set(x => x.Status, true),
                    filterUpdate.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.Users.FindOneAndUpdateAsync<Users>(filter, filterDef);
                generalModel.Id = model.UserId;
                generalModel.Status = true;
                generalModel.Message = "";
            }
            catch (Exception ex)
            {
                generalModel.Status = false;
                generalModel.Message = ex.Message;
            }
            return generalModel;
        }

        public async Task<GeneralModel> UpdateFilter(SaveUserFilter model)
        {
            GeneralModel generalModel = new GeneralModel();
            try
            {
                var filter = Builders<Users>.Filter.Where(x => x.Id == model.UserId);
                var builder = Builders<Users>.Update;
                var updatedef = builder.Combine(new UpdateDefinition<Users>[]
                {
                    builder.Set(x => x.UserName, model.UserName),
                    builder.Set(x => x.Email, model.Email),
                    builder.Set(x => x.FirstName, model.FilterName)
                });
                await _context.Users.FindOneAndUpdateAsync(filter, updatedef);
                generalModel.Status = true;
                generalModel.Message = "Filter Updated Successfully";
            }
            catch (Exception ex)
            {
                generalModel.Status = false;
                generalModel.Message = ex.Message;
            }
            return generalModel;
        }

        public async Task<List<UserList>> List()
        {
            List<UserList> userList = new List<UserList>();
            try
            {
                userList =(from U in _context.Users.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                           select new UserList
                           {
                               UserId = U.Id,
                               FirstName = U.FirstName,
                               LastName = U.LastName,
                               Email = U.Email,
                               UserName = U.UserName,
                               Status = U.Status
                           }).ToList();                         
            }
            catch
            {
                return null;
            }
            return userList;
        }

        public async Task<UserList> Details(string UserId)
        {
            UserList user = new UserList();
            try
            {
                var filter = Builders<Users>.Filter.Where(x => x.Id == UserId);
                var details = await _context.Users.Find(filter).FirstOrDefaultAsync();

                if (details != null)
                {
                    user.UserId = details.Id;
                    user.FirstName = details.FirstName;
                    user.LastName = details.LastName;
                    user.Email = details.Email;
                    user.UserName = details.UserName;
                    user.Status = details.Status;
                }
            }
            catch 
            {
                return null;
            }
            return user;
        }

        public async Task<List<UserList>> SearchList(UserSearchData search)
       {
            List<UserList> users = new List<UserList>();
            try
            {
                users = (from U in _context.Users.Find(x => x.FirstName == search.data
                                                         || x.LastName == search.data
                                                         || x.Email == search.data
                                                         || x.UserName == search.data).ToList()
                         select new UserList
                         {
                             UserId = U.Id,
                             FirstName = U.FirstName,
                             LastName = U.LastName,
                             Email = U.Email,   
                             UserName = U.UserName,
                             Status = U.Status
                         }).ToList();
            }
            catch (Exception x)
            {
                return null;
            }
            return users;
        }
    }
}
