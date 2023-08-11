using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class TaxPayerInfoRepository : ITaxPayerInfo
    {
        private readonly IDbContext _dbcontext;
        public TaxPayerInfoRepository(IDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        public async Task<GeneralModel> AddAsync(TaxPayerInfoRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<TaxPayerPersonalInfo>.Filter.Where(x => x.Email == request.Personalinfo.Email);
                var details = await _dbcontext.TaxPayerPersonalInfo.Find(filter).FirstOrDefaultAsync();

                if (details == null)
                {
                    TaxPayerPersonalInfo personalinfo = new TaxPayerPersonalInfo();
                    personalinfo = new TaxPayerPersonalInfo()
                    {
                        FilingStatusId = request.Personalinfo.FilingStatusId,
                        FirstName = request.Personalinfo.FirstName,
                        LastName = request.Personalinfo.LastName,
                        BirthDate = request.Personalinfo.Birthdate,
                        Email = request.Personalinfo.Email,
                        PhoneNumber = request.Personalinfo.PhoneNumber,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbcontext.TaxPayerPersonalInfo.InsertOneAsync(personalinfo);

                    TaxPayerAddressInfo addressinfo = new TaxPayerAddressInfo();
                    addressinfo = new TaxPayerAddressInfo()
                    {
                        TaxPayerId = personalinfo.Id,
                        AddressLine1 = request.Addressinfo.AddressLine1,
                        AddressLine2 = request.Addressinfo.AddressLine2,
                        City = request.Addressinfo.City,
                        State = request.Addressinfo.State,
                        PostalCode = request.Addressinfo.PostalCode,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbcontext.TaxPayerAddressInfo.InsertOneAsync(addressinfo);

                    TaxPayerOtherInfo otherinfo = new TaxPayerOtherInfo();
                    otherinfo = new TaxPayerOtherInfo()
                    {
                        TaxPayerId = personalinfo.Id,
                        Occupation = request.Otherinfo.Occupation,
                        FullTimeStudent = request.Otherinfo.FullTimeStudent,
                        PermanentlyDisabled = request.Otherinfo.PermanentlyDisabled,
                        LegallyBlind = request.Otherinfo.LegallyBlind,
                        IsIndependent = request.Otherinfo.IsIndependent,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbcontext.TaxPayerOtherInfo.InsertOneAsync(otherinfo);

                    model.Id = personalinfo.Id;
                    model.Status = true;
                    model.Message = "Details collected successfully";
                }
                else
                {
                    model.Status = false;
                    model.Message = "Email adready exist";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(TaxPayerUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var TPPfilter = Builders<TaxPayerPersonalInfo>.Filter.Where(x => x.Id == request.UpdatePersonalinfo.TaxPayerId);
                var TPPupdate = Builders<TaxPayerPersonalInfo>.Update;
                var TPPdetails = TPPupdate.Combine(new UpdateDefinition<TaxPayerPersonalInfo>[]
                {
                    TPPupdate.Set(x => x.FilingStatusId,request.UpdatePersonalinfo.FilingStatusId),
                    TPPupdate.Set(x => x.FirstName,request.UpdatePersonalinfo.FirstName),
                    TPPupdate.Set(x => x.LastName,request.UpdatePersonalinfo.LastName),
                    TPPupdate.Set(x => x.BirthDate,request.UpdatePersonalinfo.Birthdate),
                    TPPupdate.Set(x => x.Email,request.UpdatePersonalinfo.Email),
                    TPPupdate.Set(x => x.PhoneNumber,request.UpdatePersonalinfo.PhoneNumber),
                    TPPupdate.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbcontext.TaxPayerPersonalInfo.FindOneAndUpdateAsync(TPPfilter, TPPdetails);

                var TPAfilter = Builders<TaxPayerAddressInfo>.Filter.Where(x => x.Id == request.UpdatePersonalinfo.TaxPayerId);
                var TPAupdate = Builders<TaxPayerAddressInfo>.Update;
                var TPAdetails = TPAupdate.Combine(new UpdateDefinition<TaxPayerAddressInfo>[]
                {
                    TPAupdate.Set(x => x.AddressLine1,request.UpdateAddressinfo.AddressLine1),
                    TPAupdate.Set(x => x.AddressLine2,request.UpdateAddressinfo.AddressLine2),
                    TPAupdate.Set(x => x.City,request.UpdateAddressinfo.City),
                    TPAupdate.Set(x => x.State,request.UpdateAddressinfo.State),
                    TPAupdate.Set(x => x.PostalCode,request.UpdateAddressinfo.PostalCode),
                    TPAupdate.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbcontext.TaxPayerAddressInfo.FindOneAndUpdateAsync(TPAfilter, TPAdetails);

                var TPOfilter = Builders<TaxPayerOtherInfo>.Filter.Where(x => x.Id == request.UpdatePersonalinfo.TaxPayerId);
                var TPOupdate = Builders<TaxPayerOtherInfo>.Update;
                var TPOdetails = TPOupdate.Combine(new UpdateDefinition<TaxPayerOtherInfo>[]
                {
                    TPOupdate.Set(x => x.Occupation,request.UpdateOtherinfo.Occupation),
                    TPOupdate.Set(x => x.FullTimeStudent,request.UpdateOtherinfo.FullTimeStudent),
                    TPOupdate.Set(x => x.PermanentlyDisabled,request.UpdateOtherinfo.PermanentlyDisabled),
                    TPOupdate.Set(x => x.LegallyBlind,request.UpdateOtherinfo.LegallyBlind),
                    TPOupdate.Set(x => x.IsIndependent,request.UpdateOtherinfo.IsIndependent),
                    TPOupdate.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbcontext.TaxPayerOtherInfo.FindOneAndUpdateAsync(TPOfilter, TPOdetails);

                model.Id = request.UpdatePersonalinfo.TaxPayerId;
                model.Status = true;
                model.Message = "Details updated successfully";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<IEnumerable<TaxPayerInfoList>> GetList()
        {
            List<TaxPayerInfoList> list = new List<TaxPayerInfoList>();
            try
            {
                list = (from TPI in _dbcontext.TaxPayerPersonalInfo.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                        join TPA in _dbcontext.TaxPayerAddressInfo.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                        on TPI.Id equals TPA.TaxPayerId
                        join TPO in _dbcontext.TaxPayerOtherInfo.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                        on TPI.Id equals TPO.TaxPayerId
                        select new TaxPayerInfoList
                        {
                            TaxPayerId = TPI.Id,
                            Name = TPI.FirstName,
                            Email = TPI.Email,
                            PhoneNumber = TPI.PhoneNumber,
                            City = TPA.City,
                            Occupation = TPO.Occupation
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GeneralModel> Delete(TaxPayerActivityStatus activityStatus)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<TaxPayerPersonalInfo>.Filter.Where(x => x.Id == activityStatus.TaxPayerId);
                var updatestatus = Builders<TaxPayerPersonalInfo>.Update;
                var details = updatestatus.Combine(new UpdateDefinition<TaxPayerPersonalInfo>[]
                {
                    updatestatus.Set(x => x.Status,false),
                    updatestatus.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbcontext.TaxPayerPersonalInfo.FindOneAndUpdateAsync(filter, details);

                model.Id = activityStatus.TaxPayerId;
                model.Status = true;
                model.Message = "Status switched to InActive";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> Restore(TaxPayerActivityStatus activityStatus)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<TaxPayerPersonalInfo>.Filter.Where(x => x.Id == activityStatus.TaxPayerId);
                var updatestatus = Builders<TaxPayerPersonalInfo>.Update;
                var details = updatestatus.Combine(new UpdateDefinition<TaxPayerPersonalInfo>[]
                {
                    updatestatus.Set(x => x.Status,true),
                    updatestatus.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbcontext.TaxPayerPersonalInfo.FindOneAndUpdateAsync(filter, details);

                model.Id = activityStatus.TaxPayerId;
                model.Status = true;
                model.Message = "Status switched to Active";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }
        public async Task<List<TaxPayerActivityDTO>> ActivityList(TaxPayerActivityFilter filter, bool count)
        {
            List<TaxPayerActivityDTO> Activitylist = new List<TaxPayerActivityDTO>();
            try
            {
                var Activityfilter = Builders<TaxPayerPersonalInfo>.Filter.Where(x => x.Status == (filter.IsActive.Equals("Active", StringComparison.InvariantCultureIgnoreCase)));
                var Activitydetails = await _dbcontext.TaxPayerPersonalInfo.Find(Activityfilter).ToListAsync();

                Activitydetails.ForEach(x => Activitylist.Add(new TaxPayerActivityDTO
                {
                    TaxPayerId = x.Id,
                    Name = x.FirstName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    TaxPayerStatus = x.Status ? "Active" : "InActive",
                    CreatedDate = x.CreatedDate,
                    UpdateDate = x.UpdateDate
                }));

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    Activitydetails = Activitydetails.Where(x => x.Email.Contains(filter.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(filter.PhoneNumber))
                {
                    Activitydetails = Activitydetails.Where(x => x.PhoneNumber.Contains(filter.PhoneNumber, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                Activitydetails = Activitydetails.OrderByDescending(x => x.UpdateDate).ToList();

                if (!count)
                {
                    Activitydetails = Activitydetails.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return Activitylist;
        }
    }
}
