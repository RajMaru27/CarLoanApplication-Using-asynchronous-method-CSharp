using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class TaxPayerFilingStatusRepository : ITaxPayerFilingStatusRepository
    {
        private readonly IDbContext _dbcontext;
        public TaxPayerFilingStatusRepository(IDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        public async Task<GeneralModel> AddAsync(TPFillingStatusRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<TaxPayerFilingStatus>.Filter.Where(x => x.Name == request.Name);
                var details = await _dbcontext.TaxPayerFilingStatus.Find(filter).FirstOrDefaultAsync();

                if (details == null)
                {
                    TaxPayerFilingStatus filingstatus = new TaxPayerFilingStatus();
                    filingstatus = new TaxPayerFilingStatus
                    {
                        Name = request.Name,
                        Status = true,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                    };
                    await _dbcontext.TaxPayerFilingStatus.InsertOneAsync(filingstatus);

                    model.Id = filingstatus.Id;
                    model.Status = true;
                    model.Message = "Status Created Successfully";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<List<TPFilingStatusList>> GetListAsync()
        {
            List<TPFilingStatusList> list = new List<TPFilingStatusList>();
            var Status = await _dbcontext.TaxPayerFilingStatus.Find(x => !string.IsNullOrEmpty(x.Name)).ToListAsync();
            Status.ForEach(x => list.Add(new TPFilingStatusList
            {
                Id = x.Id,
                Name = x.Name
            }));
            return list;
        }
    }
}
