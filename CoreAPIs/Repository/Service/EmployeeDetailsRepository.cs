using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class EmployeeDetailsRepository : IEmployeeDetails
    {
        private readonly IDbContext _context;
        public EmployeeDetailsRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GeneralModel> AddAsync(EmployeelDetailRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<EmployeePersonalDetails>.Filter.Where(x => x.Email == request.Personaldetails.Email);
                var details = await _context.EmployeePersonalDetails.Find(filter).FirstOrDefaultAsync();

                if (details == null)
                {
                    EmployeePersonalDetails personalDetails = new EmployeePersonalDetails();
                    personalDetails = new EmployeePersonalDetails()
                    {
                        Name = request.Personaldetails.Name,
                        Age = request.Personaldetails.Age,
                        BirthDate = request.Personaldetails.BirthDate,
                        Address = request.Personaldetails.Address,
                        Email = request.Personaldetails.Email,
                        Phone = request.Personaldetails.Phone,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _context.EmployeePersonalDetails.InsertOneAsync(personalDetails);

                    EmployeeWorkDetail workDetail = new EmployeeWorkDetail();
                    workDetail = new EmployeeWorkDetail()
                    {
                        EmployeeId = personalDetails.Id,
                        Department = request.Workdetails.Department,
                        Designation = request.Workdetails.Designation,
                        WorkingSince = request.Workdetails.WorkingSince,
                        AnnualSalary = request.Workdetails.AnnualSalary,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _context.EmployeeWorkDetail.InsertOneAsync(workDetail);

                    EmployeeEducationDetails educationDetails = new EmployeeEducationDetails();
                    educationDetails = new EmployeeEducationDetails()
                    {
                        EmployeeId = personalDetails.Id,
                        Qualification = request.Educationdetails.Qualification,
                        QualificationYear = request.Educationdetails.QualificationYear,
                        University = request.Educationdetails.University,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _context.EmployeeEducationDetails.InsertOneAsync(educationDetails);

                    model.Id = personalDetails.Id;
                    model.Status = true;
                    model.Message = "Data Created Successfully";
                }
                else
                {
                    model.Status = false;
                    model.Message = "Email already exist";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(UpdateEmployeeDetails request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var Personaldetailfilter = Builders<EmployeePersonalDetails>.Filter.Where(x => x.Id == request.UpdatePersonaldetails.Id);
                var updatedetails = Builders<EmployeePersonalDetails>.Update;
                var personaldetails = updatedetails.Combine(new UpdateDefinition<EmployeePersonalDetails>[]
                {
                    updatedetails.Set(x => x.Name,request.UpdatePersonaldetails.Name),
                    updatedetails.Set(x => x.Age,request.UpdatePersonaldetails.Age),
                    updatedetails.Set(x => x.BirthDate,request.UpdatePersonaldetails.BirthDate),
                    updatedetails.Set(x => x.Address,request.UpdatePersonaldetails.Address),
                    updatedetails.Set(x => x.Email,request.UpdatePersonaldetails.Email),
                    updatedetails.Set(x => x.Phone,request.UpdatePersonaldetails.Phone),
                    updatedetails.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.EmployeePersonalDetails.FindOneAndUpdateAsync(Personaldetailfilter, personaldetails);

                var workdetailfilter = Builders<EmployeeWorkDetail>.Filter.Where(x => x.EmployeeId == request.UpdatePersonaldetails.Id);
                var updateworkdetails = Builders<EmployeeWorkDetail>.Update;
                var workdetail = updateworkdetails.Combine(new UpdateDefinition<EmployeeWorkDetail>[]
                {
                    updateworkdetails.Set(x => x.Department,request.UpdateWorkdetails.Department),
                    updateworkdetails.Set(x => x.Designation,request.UpdateWorkdetails.Designation),
                    updateworkdetails.Set(x => x.WorkingSince,request.UpdateWorkdetails.WorkingSince),
                    updateworkdetails.Set(x => x.AnnualSalary,request.UpdateWorkdetails.AnnualSalary),
                    updateworkdetails.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.EmployeeWorkDetail.FindOneAndUpdateAsync (workdetailfilter, workdetail);

                var educationdetailfilter = Builders<EmployeeEducationDetails>.Filter.Where(x => x.EmployeeId == request.UpdatePersonaldetails.Id);
                var updateeducationdetails = Builders<EmployeeEducationDetails>.Update;
                var educationdetails = updateeducationdetails.Combine(new UpdateDefinition<EmployeeEducationDetails>[]
                {
                    updateeducationdetails.Set(x => x.Qualification,request.UpdateEducationdetails.Qualification),
                    updateeducationdetails.Set(x => x.QualificationYear,request.UpdateEducationdetails.QualificationYear),
                    updateeducationdetails.Set(x => x.University,request.UpdateEducationdetails.University),
                    updateeducationdetails.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.EmployeeEducationDetails.FindOneAndUpdateAsync(educationdetailfilter, educationdetails);

                model.Id = request.UpdatePersonaldetails.Id;
                model.Status = true;
                model.Message = "Data updated successfully";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<IEnumerable<EmployeeDetailList>> GetAll()
        {
            List<EmployeeDetailList> list = new List<EmployeeDetailList>();
            try
            {
                list = (from a in _context.EmployeePersonalDetails.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                       join b in _context.EmployeeWorkDetail.Find(x => !string.IsNullOrEmpty(x.EmployeeId)).ToList()
                       on a.Id equals b.EmployeeId
                       join c in _context.EmployeeEducationDetails.Find(x => !string.IsNullOrEmpty(x.EmployeeId)).ToList()
                       on a.Id equals c.EmployeeId
                       select new EmployeeDetailList
                       {
                           EmployeeId = a.Id,
                           Name = a.Name,
                           Address = a.Address,
                           Email = a.Email,
                           Phone = a.Phone,
                           Department = b.Department,
                           Designation = b.Designation,
                           Qualification = c.Qualification
                       }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return list;
        }

        public async Task<GeneralModel> DeleteAsync(EmployeeStatus empstatus)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<EmployeePersonalDetails>.Filter.Where(x => x.Id == empstatus.EmployeeId);
                var deactivateId = Builders<EmployeePersonalDetails>.Update;
                var details = deactivateId.Combine(new UpdateDefinition<EmployeePersonalDetails>[]
                {
                    deactivateId.Set(x => x.Status,false),
                    deactivateId.Set(x => x.UpdateDate,DateTime.UtcNow),
                });
                await _context.EmployeePersonalDetails.FindOneAndUpdateAsync(filter, details);

                model.Id = empstatus.EmployeeId;
                model.Status = true;
                model.Message = empstatus.EmployeeId + " " + "is Deactivated";
            }
            catch (Exception ex) 
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> Restore(EmployeeStatus empstatus)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<EmployeePersonalDetails>.Filter.Where(x => x.Id == empstatus.EmployeeId);
                var Activate = Builders<EmployeePersonalDetails>.Update;
                var details = Activate.Combine(new UpdateDefinition<EmployeePersonalDetails>[]
                {
                    Activate.Set(x => x.Status, true),
                    Activate.Set(x => x.UpdateDate, DateTime.UtcNow),
                });
                await _context.EmployeePersonalDetails.FindOneAndUpdateAsync (filter, details);

                model.Id = empstatus.EmployeeId;
                model.Status = true;
                model.Message = empstatus.EmployeeId + " " + "is Activated";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message; 
            }
            return model;
        }

        public async Task<IEnumerable<EmployeeActivityDTO>> ActivityList(EmployeeActivityFilter filter, bool Count)
        {
            List<EmployeeActivityDTO> list = new List<EmployeeActivityDTO>();
            try
            {
                var Activityfilter = Builders<EmployeePersonalDetails>.Filter.Where(x => x.Status == (filter.IsActive.Equals("Active", StringComparison.InvariantCultureIgnoreCase)));
                var Activitydetails = await _context.EmployeePersonalDetails.Find(Activityfilter).ToListAsync();

                Activitydetails.ForEach(x => list.Add(new EmployeeActivityDTO
                {
                    EmployeeId = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    EmpStatus = x.Status ? "Active" : "InActive",
                    CreatedDate = x.CreatedDate,
                    UpdateDate = x.UpdateDate
                }));

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    list = list.Where(x => x.Email.Contains(filter.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(filter.Phone))
                {
                    list = list.Where(x => x.Phone.Contains(filter.Phone, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }

                list = list.OrderByDescending(x => x.UpdateDate).ToList();

                if (!Count)
                {
                    list = list.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return list;
        }
    }
}
