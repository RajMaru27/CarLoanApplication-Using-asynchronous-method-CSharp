using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IDbContext _context;
        public BranchRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GeneralModel> AddAsync(BranchRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var Branchfilter = Builders<Branch>.Filter.Where(x => x.Id == request.Id);
                var Branchdetail = await _context.Branches.Find(Branchfilter).FirstOrDefaultAsync();

                if (Branchdetail == null)
                {
                    Branch branch = new Branch();
                    branch = new Branch
                    {
                        BranchName = request.BranchName,
                        Address = request.Address,
                        City = request.City,
                        State = request.State,
                        Pincode = request.Pincode,
                        country = request.country
                    };
                    await _context.Branches.InsertOneAsync(branch);

                    model = new GeneralModel()
                    {
                        Id = request.Id,
                        Status = true,
                        Message = "Branch Added"
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

        public async Task<GeneralModel> UpdateAsync(BranchRequest request)
        {
            GeneralModel model = new GeneralModel();
            try


            {
                var Filter = Builders<Branch>.Filter.Where(x => x.Id == request.Id);

                var Updatebuilder = Builders<Branch>.Update;
                var UpdateDef = Updatebuilder.Combine(new UpdateDefinition<Branch>[]
                {
                    Updatebuilder.Set(x => x.BranchName,request.BranchName),
                    Updatebuilder.Set(x => x.Address,request.Address),
                    Updatebuilder.Set(x => x.City,request.City),
                    Updatebuilder.Set(x => x.State,request.State),
                    Updatebuilder.Set(x => x.Pincode,request.Pincode),
                    Updatebuilder.Set(x => x.country,request.country)
                });
                await _context.Branches.FindOneAndUpdateAsync(Filter, UpdateDef);

                model.Id = request.Id;
                model.Status = true;
                model.Message = "Details Updated";
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
