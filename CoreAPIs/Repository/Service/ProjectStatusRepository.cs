using CoreAPIs.Context;
using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly IDbContext _context;
        public ProjectStatusRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<GeneralModel> AddAsync(StatusRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<ProjectStatus>.Filter.Where(x => x.StatusName == request.StatusName);
                var details = await _context.Projectstatus.Find(filter).FirstOrDefaultAsync();

                if (details == null)
                {
                    ProjectStatus status = new ProjectStatus();
                    status = new ProjectStatus
                    {
                        StatusName = request.StatusName,
                        Status = true,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                    };
                    await _context.Projectstatus.InsertOneAsync(status);
                    model.Id = status.Id;
                    model.Status = true;
                    model.Message = "";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }
        public async Task<IEnumerable<StatusRequest>> GetListAsync()
        {
            List<StatusRequest> list = new List<StatusRequest>();
            var status = await _context.Projectstatus.Find(x => !string.IsNullOrEmpty(x.StatusName)).ToListAsync();
            status.ForEach(x => list.Add(new StatusRequest
            {
                id = x.Id,
                StatusName = x.StatusName,
            }));
            return list;
        }
    }
}
