using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class TPSpouseInfoRepository : ITPSpouseInfo
    {
        private readonly IDbContext _dbContext;
        public TPSpouseInfoRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<GeneralModel> AddAsync(TPSpouseInfoRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<TPSpounsePersonalInfo>.Filter.Where(x => x.Email == request.SpousePersonalinfo.Email);
                var details = await _dbContext.TPSpounsePersonalInfo.Find(filter).FirstOrDefaultAsync();

                if (details == null)
                {
                    TPSpounsePersonalInfo personalinfo = new TPSpounsePersonalInfo();
                    personalinfo = new TPSpounsePersonalInfo
                    {
                        TaxPayerId = request.SpousePersonalinfo.TaxPayerId,
                        FirstName = request.SpousePersonalinfo.FirstName,
                        LastName = request.SpousePersonalinfo.LastName,
                        BirthDate = request.SpousePersonalinfo.Birthdate,
                        Email = request.SpousePersonalinfo.Email,
                        PhoneNumber = request.SpousePersonalinfo.PhoneNumber,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _dbContext.TPSpounsePersonalInfo.InsertOneAsync(personalinfo);

                    TPSpouseAddressInfo addressInfo = new TPSpouseAddressInfo();
                    addressInfo = new TPSpouseAddressInfo
                    {
                        SpouseId = personalinfo.Id,
                        AddressLine1 = request.SpouseAddressinfo.AddressLine1,
                        AddressLine2 = request.SpouseAddressinfo.AddressLine2,
                        City = request.SpouseAddressinfo.City,
                        State = request.SpouseAddressinfo.State,
                        PostalCode = request.SpouseAddressinfo.PostalCode,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _dbContext.TPSpouseAddressInfo.InsertOneAsync(addressInfo);

                    TPSpouseOtherInfo otherInfo = new TPSpouseOtherInfo();
                    otherInfo = new TPSpouseOtherInfo
                    {
                        SpouseId = personalinfo.Id,
                        Occupation = request.SpouseOtherinfo.Occupation,
                        FullTimeStudent = request.SpouseOtherinfo.FullTimeStudent,
                        PermanentlyDisabled = request.SpouseOtherinfo.PermanentlyDisabled,
                        LegallyBlind = request.SpouseOtherinfo.LegallyBlind,
                        IsIndependent = request.SpouseOtherinfo.IsIndependent,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    model.Id = personalinfo.Id;
                    model.Status = personalinfo.Status;
                    model.Message = "Data added successfully";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(TPSpouseInfoUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<TPSpounsePersonalInfo>.Filter.Where(x => x.Id == request.UpdateSpousePersonalinfo.SpouseId);
                var updateinfo = Builders<TPSpounsePersonalInfo>.Update;
                var details = updateinfo.Combine(new UpdateDefinition<TPSpounsePersonalInfo>[]
                {
                    updateinfo.Set(x => x.FirstName, request.UpdateSpousePersonalinfo.FirstName),
                    updateinfo.Set(x => x.LastName, request.UpdateSpousePersonalinfo.LastName),
                    updateinfo.Set(x => x.BirthDate, request.UpdateSpousePersonalinfo.Birthdate),
                    updateinfo.Set(x => x.Email, request.UpdateSpousePersonalinfo.Email),
                    updateinfo.Set(x => x.PhoneNumber, request.UpdateSpousePersonalinfo.PhoneNumber),
                    updateinfo.Set(x => x.UpdateDate, DateTime.UtcNow)
                });
                await _dbContext.TPSpounsePersonalInfo.FindOneAndUpdateAsync(filter, details);

                var addressfilter = Builders<TPSpouseAddressInfo>.Filter.Where(x => x.Id == request.UpdateSpousePersonalinfo.SpouseId);
                var updateaddress = Builders<TPSpouseAddressInfo>.Update;
                var addressdetails = updateaddress.Combine(new UpdateDefinition<TPSpouseAddressInfo>[]
                {
                    updateaddress.Set(x => x.AddressLine1,request.UpdateSpouseAddressinfo.AddressLine1),
                    updateaddress.Set(x => x.AddressLine2,request.UpdateSpouseAddressinfo.AddressLine2),
                    updateaddress.Set(x => x.City,request.UpdateSpouseAddressinfo.City),
                    updateaddress.Set(x => x.State,request.UpdateSpouseAddressinfo.State),
                    updateaddress.Set(x => x.PostalCode,request.UpdateSpouseAddressinfo.PostalCode),
                    updateaddress.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.TPSpouseAddressInfo.FindOneAndUpdateAsync(addressfilter, addressdetails);

                var otherinfofilter = Builders<TPSpouseOtherInfo>.Filter.Where(x => x.Id == request.UpdateSpousePersonalinfo.SpouseId);
                var updateotherinfo = Builders<TPSpouseOtherInfo>.Update;
                var otherdetails = updateotherinfo.Combine(new UpdateDefinition<TPSpouseOtherInfo>[]
                {
                    updateotherinfo.Set(x => x.Occupation,request.UpdateSpouseOtherinfo.Occupation),
                    updateotherinfo.Set(x => x.FullTimeStudent,request.UpdateSpouseOtherinfo.FullTimeStudent),
                    updateotherinfo.Set(x => x.PermanentlyDisabled,request.UpdateSpouseOtherinfo.PermanentlyDisabled),
                    updateotherinfo.Set(x => x.LegallyBlind,request.UpdateSpouseOtherinfo.LegallyBlind),
                    updateotherinfo.Set(x => x.IsIndependent,request.UpdateSpouseOtherinfo.IsIndependent),
                    updateotherinfo.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.TPSpouseOtherInfo.FindOneAndUpdateAsync(otherinfofilter, otherdetails);

                model.Id = request.UpdateSpousePersonalinfo.SpouseId;
                model.Status = true;
                model.Message = "Updated";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }
    }
}
