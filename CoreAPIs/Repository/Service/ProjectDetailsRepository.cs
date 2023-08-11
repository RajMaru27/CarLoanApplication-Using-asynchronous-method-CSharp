using CoreAPIs.Context;
using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class ProjectDetailsRepository : IProjectDetailsRepository
    {
        private readonly IDbContext _context;
        public ProjectDetailsRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GeneralModel> AddAsync(ProjectDetailRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<ProjectDetails>.Filter.Where(x => x.Name == request.Name);
                var filterdetail = await _context.Projectdetails.Find(filter).FirstOrDefaultAsync();

                if (filterdetail == null)
                {
                    ProjectDetails details = new ProjectDetails();
                    details = new ProjectDetails
                    {
                        Name = request.Name,
                        Description = request.Description,
                        StatusId = request.StatusId,
                        ClientCompany = request.ClientCompany,
                        ProjectLeader = request.ProjectLeader,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };

                    await _context.Projectdetails.InsertOneAsync(details);
                    model.Id = details.Id;
                    model.Status = true;
                    model.Message = "Created";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(ProjectDetailRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<ProjectDetails>.Filter.Where(x => x.Name == request.Name);

                var filterUpdate = Builders<ProjectDetails>.Update;
                var filderdef = filterUpdate.Combine(new UpdateDefinition<ProjectDetails>[]
                {
                    filterUpdate.Set(x => x.Name, request.Name),
                    filterUpdate.Set(x => x.Description, request.Description),
                    filterUpdate.Set(x => x.StatusId, request.StatusId),
                    filterUpdate.Set(x => x.ClientCompany, request.ClientCompany),
                    filterUpdate.Set(x => x.ProjectLeader, request.ProjectLeader),
                    filterUpdate.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.Projectdetails.FindOneAndUpdateAsync(filter, filderdef);
                model.Id = request.Id;
                model.Status = true;
                model.Message = "Update Successfull";
            }
            catch (Exception ex)
            {
                model.Status = true;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> DeleteAsync(ProDetailStatusUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<ProjectDetails>.Filter.Where(x => x.Id == request.Id);

                var filterUpdate = Builders<ProjectDetails>.Update;
                var filterdef = filterUpdate.Combine(new UpdateDefinition<ProjectDetails>[]
                {
                    filterUpdate.Set(x => x.Status, false),
                    filterUpdate.Set(x => x.UpdateDate, DateTime.UtcNow),
                });
                await _context.Projectdetails.FindOneAndUpdateAsync(filter, filterdef);
                model.Id = request.Id;
                model.Status = true;
                model.Message = "";
            }
            catch (Exception e)
            {
                model.Status = false;
                model.Message = e.Message;
            }
            return model;
        }

        public async Task<GeneralModel> Restore(ProDetailStatusUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<ProjectDetails>.Filter.Where(x => x.Id == request.Id);

                var filterUpdate = Builders<ProjectDetails>.Update;
                var filterdef = filterUpdate.Combine(new UpdateDefinition<ProjectDetails>[]
                {
                    filterUpdate.Set(x => x.Status, true),
                    filterUpdate.Set(x => x.UpdateDate, DateTime.UtcNow),
                });
                await _context.Projectdetails.FindOneAndUpdateAsync(filter, filterdef);
                model.Id = request.Id;
                model.Status = true;
                model.Message = "";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<IEnumerable<ProDetailStatus>> List()
        {
            List<ProDetailStatus> detail = new List<ProDetailStatus>();
            try
            {
                detail = (from a in _context.Projectdetails.Find(x => true).ToList()
                          join b in _context.Projectstatus.Find(x => true).ToList() on a.StatusId equals b.Id
                          select new ProDetailStatus
                          {
                              ClientCompany = a.ClientCompany,
                              Description = a.Description,
                              id = a.Id,
                              Name = a.Name,
                              ProjectLeader = a.ProjectLeader,
                              Status = a.Status,
                              StatusName = b.StatusName
                          }).ToList();
            }
            catch (Exception ex)
            {
                return null;                   
            }
            return detail;
        }
    }
}
