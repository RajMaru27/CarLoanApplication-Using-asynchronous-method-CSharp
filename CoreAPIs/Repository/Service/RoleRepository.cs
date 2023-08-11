using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContext _context;
        public RoleRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<GeneralModel> AddAsync(RoleRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var RoleFilter = Builders<Role>.Filter.Where(x => x.Id == request.Id);
                var Roledetail = await _context.Roles.Find(RoleFilter).FirstOrDefaultAsync();

                if (Roledetail == null)
                {
                    Role role = new Role();
                    role = new Role
                    {
                        RoleName = request.RoleName,
                        Salary = request.Salary,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };
                    await _context.Roles.InsertOneAsync(role);

                    model = new GeneralModel()
                    {
                        Id = request.Id,
                        Status = true,
                        Message = "Role Added"
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
        public async Task<GeneralModel> UpdateAsync (RoleRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var Filter = Builders<Role>.Filter.Where(x => x.Id == request.Id );

                var Updatebuilder = Builders<Role>.Update;
                var UpdateDef = Updatebuilder.Combine(new UpdateDefinition<Role>[]
                {
                    Updatebuilder.Set(x => x.RoleName, request.RoleName),
                    Updatebuilder.Set(x => x.Salary, request.Salary),
                    Updatebuilder.Set(x => x.UpdateDate, DateTime.UtcNow),
                });
                await _context.Roles.FindOneAndUpdateAsync(Filter, UpdateDef);
                model.Id = request.Id;
                model.Status = true;
                model.Message = "Role Updated";
                return model;
            }
            catch (Exception ex)
            {
                model.Status = true;
                model.Message = ex.Message;
            }
            return model;
        }
    }
}
