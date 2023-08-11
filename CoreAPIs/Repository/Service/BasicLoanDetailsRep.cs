using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CoreAPIs.Repository.Service
{
    public class BasicLoanDetailsRep : IBasicLoanDetailsRep
    {
        private readonly IDbContext _dbContext;
        public BasicLoanDetailsRep(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<GeneralModel> AddAsync(LoanDetailRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<BasicLoanDetails>.Filter.Where(x => x.Email == request.basicDetails.Email);
                var details = await _dbContext.BasicLoanDetails.Find(filter).FirstOrDefaultAsync();

                if (details == null)
                {
                    BasicLoanDetails Basicdetails = new BasicLoanDetails();
                    Basicdetails = new BasicLoanDetails
                    {
                        FirstName = request.basicDetails.FirstName,
                        LastName = request.basicDetails.LastName,
                        DateOfBirth = request.basicDetails.DateOfBirth,
                        MaritalStatus = request.basicDetails.MaritalStatus,
                        Email = request.basicDetails.Email,
                        PhoneNumber = request.basicDetails.PhoneNumber,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbContext.BasicLoanDetails.InsertOneAsync(Basicdetails);

                    LoanAddressDetails addressDetails = new LoanAddressDetails();
                    addressDetails = new LoanAddressDetails
                    {
                        LoanId = Basicdetails.Id,
                        Line1 = request.addressDetails.Line1,
                        Line2 = request.addressDetails.Line2,
                        City = request.addressDetails.City,
                        State = request.addressDetails.State,
                        ZipCode = request.addressDetails.ZipCode,
                        LivingSince = request.addressDetails.LivingSince,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbContext.LoanAddressDetails.InsertOneAsync(addressDetails);

                    RequiredLoanDetails loanDetails = new RequiredLoanDetails();
                    loanDetails = new RequiredLoanDetails
                    {
                        LoanId = Basicdetails.Id,
                        DesiredLoanAmt = request.loanDetails.DesiredLoanAmt,
                        AnnualIncome = request.loanDetails.AnnualIncome,
                        LoanType = request.loanDetails.LoanType,
                        Downpayment = request.loanDetails.Downpayment,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbContext.RequiredLoanDetails.InsertOneAsync(loanDetails);

                    EmploymentDetails employmentDetails = new EmploymentDetails();
                    employmentDetails = new EmploymentDetails
                    {
                        LoanId = Basicdetails.Id,
                        EmployerName = request.employmentDetails.EmployerName,
                        Occupation = request.employmentDetails.Occupation,
                        WorkExperience = request.employmentDetails.WorkExperience,
                        GrossMonthlyIncome = request.employmentDetails.GrossMonthlyIncome,
                        MonthlyExpense = request.employmentDetails.MonthlyExpense,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbContext.EmploymentDetails.InsertOneAsync(employmentDetails);

                    BankDetails bankDetails = new BankDetails();
                    bankDetails = new BankDetails
                    {
                        LoanId = Basicdetails.Id,
                        BankName = request.bankDetails.BankName,
                        AccNo = request.bankDetails.AccNo,
                        Address = request.bankDetails.Address,
                        PhoneNumber = request.bankDetails.PhoneNumber,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _dbContext.BankDetails.InsertOneAsync(bankDetails);

                    model.Id = Basicdetails.Id;
                    model.Status = true;
                    model.Message = "Success";

                }

            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(UpdateLoanDetails request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                BasicLoanDetails basicDetail = new BasicLoanDetails();
                var filter = Builders<BasicLoanDetails>.Filter.Where(x => x.Id == request.basicDetails.Id);
                var UpdateBasicDetails = Builders<BasicLoanDetails>.Update;
                var Updatedetails = UpdateBasicDetails.Combine(new UpdateDefinition<BasicLoanDetails>[]
                {
                    UpdateBasicDetails.Set(x => x.FirstName, request.basicDetails.FirstName),
                    UpdateBasicDetails.Set(x => x.LastName, request.basicDetails.LastName),
                    UpdateBasicDetails.Set(x => x.DateOfBirth, request.basicDetails.DateOfBirth),
                    UpdateBasicDetails.Set(x => x.MaritalStatus, request.basicDetails.MaritalStatus),
                    UpdateBasicDetails.Set(x => x.Email, request.basicDetails.Email),
                    UpdateBasicDetails.Set(x => x.PhoneNumber, request.basicDetails.PhoneNumber),
                    UpdateBasicDetails.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.BasicLoanDetails.FindOneAndUpdateAsync(filter, Updatedetails);

                var Addressfilter = Builders<LoanAddressDetails>.Filter.Where(x => x.LoanId == request.basicDetails.Id);
                var UpdateAddress = Builders<LoanAddressDetails>.Update;
                var AddressDetails = UpdateAddress.Combine(new UpdateDefinition<LoanAddressDetails>[]
                {
                    UpdateAddress.Set(x => x.Line1, request.addressDetails.Line1),
                    UpdateAddress.Set(x => x.Line2, request.addressDetails.Line2),
                    UpdateAddress.Set(x => x.City, request.addressDetails.City),
                    UpdateAddress.Set(x => x.State, request.addressDetails.State),
                    UpdateAddress.Set(x => x.ZipCode, request.addressDetails.ZipCode),
                    UpdateAddress.Set(x => x.LivingSince, request.addressDetails.LivingSince),
                    UpdateAddress.Set(x => x.UpdateDate, DateTime.UtcNow)
                });
                await _dbContext.LoanAddressDetails.FindOneAndUpdateAsync(Addressfilter, AddressDetails);

                var LoandetailFilter = Builders<RequiredLoanDetails>.Filter.Where(x => x.LoanId == request.basicDetails.Id);
                var UpdateLoandetail = Builders<RequiredLoanDetails>.Update;
                var LoanDetail = UpdateLoandetail.Combine(new UpdateDefinition<RequiredLoanDetails>[]
                {
                    UpdateLoandetail.Set(x => x.DesiredLoanAmt, request.loanDetails.DesiredLoanAmt),
                    UpdateLoandetail.Set(x => x.AnnualIncome, request.loanDetails.AnnualIncome),
                    UpdateLoandetail.Set(x => x.LoanType, request.loanDetails.LoanType),
                    UpdateLoandetail.Set(x => x.Downpayment, request.loanDetails.Downpayment),
                    UpdateLoandetail.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.RequiredLoanDetails.FindOneAndUpdateAsync(LoandetailFilter, LoanDetail);

                var EmpDetailfilter = Builders<EmploymentDetails>.Filter.Where(x => x.LoanId == request.basicDetails.Id);
                var UpdateEmpDetail = Builders<EmploymentDetails>.Update;
                var EmpDetail = UpdateEmpDetail.Combine(new UpdateDefinition<EmploymentDetails>[]
                {
                    UpdateEmpDetail.Set(x => x.EmployerName, request.employmentDetails.EmployerName),
                    UpdateEmpDetail.Set(x => x.Occupation, request.employmentDetails.Occupation),
                    UpdateEmpDetail.Set(x => x.WorkExperience, request.employmentDetails.WorkExperience),
                    UpdateEmpDetail.Set(x => x.GrossMonthlyIncome, request.employmentDetails.GrossMonthlyIncome),
                    UpdateEmpDetail.Set(x => x.MonthlyExpense, request.employmentDetails.MonthlyExpense),
                    UpdateEmpDetail.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.EmploymentDetails.FindOneAndUpdateAsync(EmpDetailfilter, EmpDetail);

                var BankDetailfilter = Builders<BankDetails>.Filter.Where(x => x.LoanId == request.basicDetails.Id);
                var UpdateBankDetail = Builders<BankDetails>.Update;
                var BankDetail = UpdateBankDetail.Combine(new UpdateDefinition<BankDetails>[]
                {
                    UpdateBankDetail.Set(x => x.BankName, request.bankDetails.BankName),
                    UpdateBankDetail.Set(x => x.AccNo, request.bankDetails.AccNo),
                    UpdateBankDetail.Set(x => x.Address, request.bankDetails.Address),
                    UpdateBankDetail.Set(x => x.PhoneNumber, request.bankDetails.PhoneNumber),
                    UpdateBankDetail.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.BankDetails.FindOneAndUpdateAsync(BankDetailfilter, BankDetail);

                model.Id = request.basicDetails.Id;
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

        public async Task<GeneralModel> DeleteAsync(LoanStatusUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<BasicLoanDetails>.Filter.Where(x => x.Id == request.LoanId);
                var Statusupdate = Builders<BasicLoanDetails>.Update;
                var status = Statusupdate.Combine(new UpdateDefinition<BasicLoanDetails>[]
                {
                    Statusupdate.Set(x => x.Status,false),
                    Statusupdate.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _dbContext.BasicLoanDetails.FindOneAndUpdateAsync(filter, status);

                model.Id = request.LoanId;
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

        public async Task<GeneralModel> Restore(LoanStatusUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<BasicLoanDetails>.Filter.Where(x => x.Id == request.LoanId);
                var StatusUpdate = Builders<BasicLoanDetails>.Update;
                var Status = StatusUpdate.Combine(new UpdateDefinition<BasicLoanDetails>[]
                {
                    StatusUpdate.Set(x => x.Status, true),
                    StatusUpdate.Set(x => x.UpdateDate, DateTime.UtcNow)
                });
                await _dbContext.BasicLoanDetails.FindOneAndUpdateAsync(filter, Status);

                model.Id = request.LoanId;
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

        public async Task<IEnumerable<LoanDetailsList>> GetAll()
        {
            List<LoanDetailsList> details = new List<LoanDetailsList>();
            try
            {
                details = (from a in _dbContext.BasicLoanDetails.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                           join b in _dbContext.RequiredLoanDetails.Find(x => !string.IsNullOrEmpty(x.LoanId)).ToList()
                           on a.Id equals b.LoanId
                           select new LoanDetailsList
                           {
                               LoanId = a.Id,
                               FirstName = a.FirstName,
                               LastName = a.LastName,
                               DateOfBirth = a.DateOfBirth,
                               Email = a.Email,
                               PhoneNumber = a.PhoneNumber,
                               DesiredLoanAMT = Convert.ToString(b.DesiredLoanAmt),
                               LoanType = b.LoanType
                           }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return details;
        }

        public async Task<LoanDetailRequest> DetailAsync(string id)
        {
            LoanDetailRequest details = new LoanDetailRequest();
            try
            {
                if (id != null)
                {
                    var filterbasicdetail = Builders<BasicLoanDetails>.Filter.Where(x => x.Id == id);
                    var basicdetails = await _dbContext.BasicLoanDetails.Find(filterbasicdetail).FirstOrDefaultAsync();
                    details.basicDetails = new LoanDetailRequest.BasicDetails();
                    details.basicDetails.FirstName = basicdetails.FirstName;
                    details.basicDetails.LastName = basicdetails.LastName;
                    details.basicDetails.DateOfBirth = basicdetails.DateOfBirth;
                    details.basicDetails.MaritalStatus = basicdetails.MaritalStatus;
                    details.basicDetails.Email = basicdetails.Email;
                    details.basicDetails.PhoneNumber = basicdetails.PhoneNumber;

                    var filterAddress = Builders<LoanAddressDetails>.Filter.Where(x => x.LoanId == id);
                    var Addressdetails = await _dbContext.LoanAddressDetails.Find(filterAddress).FirstOrDefaultAsync();
                    details.addressDetails = new LoanDetailRequest.AddressDetails();
                    details.addressDetails.Line1 = Addressdetails.Line1;
                    details.addressDetails.Line2 = Addressdetails.Line2;
                    details.addressDetails.City = Addressdetails.City;
                    details.addressDetails.State = Addressdetails.State;
                    details.addressDetails.ZipCode = Addressdetails.ZipCode;
                    details.addressDetails.LivingSince = Addressdetails.LivingSince;

                    var filterloandetail = Builders<RequiredLoanDetails>.Filter.Where(x => x.LoanId == id);
                    var Loandetails = await _dbContext.RequiredLoanDetails.Find(filterloandetail).FirstOrDefaultAsync();
                    details.loanDetails = new LoanDetailRequest.RequiredLoanDetails();
                    details.loanDetails.DesiredLoanAmt = Loandetails.DesiredLoanAmt;
                    details.loanDetails.AnnualIncome = Loandetails.AnnualIncome;
                    details.loanDetails.LoanType = Loandetails.LoanType;
                    details.loanDetails.Downpayment = Loandetails.Downpayment;

                    var filterEmployment = Builders<EmploymentDetails>.Filter.Where(x => x.LoanId == id);
                    var Employmentdetails = await _dbContext.EmploymentDetails.Find(filterEmployment).FirstOrDefaultAsync();
                    details.employmentDetails = new LoanDetailRequest.EmploymentDetails();
                    details.employmentDetails.EmployerName = Employmentdetails.EmployerName;
                    details.employmentDetails.Occupation = Employmentdetails.Occupation;
                    details.employmentDetails.WorkExperience = Employmentdetails.WorkExperience;
                    details.employmentDetails.GrossMonthlyIncome = Employmentdetails.GrossMonthlyIncome;
                    details.employmentDetails.MonthlyExpense = Employmentdetails.MonthlyExpense;

                    var filterBank = Builders<BankDetails>.Filter.Where(x => x.LoanId == id);
                    var Bankdetails = await _dbContext.BankDetails.Find(filterBank).FirstOrDefaultAsync();
                    details.bankDetails = new LoanDetailRequest.BankDetails();
                    details.bankDetails.BankName = Bankdetails.BankName;
                    details.bankDetails.AccNo = Bankdetails.AccNo;
                    details.bankDetails.Address = Bankdetails.Address;
                    details.bankDetails.PhoneNumber = Bankdetails.PhoneNumber;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
            return details;
        }

        public async Task<List<LoanActivityDTO>> GetActivity(LoanActivityFilter filter, bool Count)
        {
            List<LoanActivityDTO> Activelist = new List<LoanActivityDTO>();
            try
            {
                var Activityfilter = Builders<BasicLoanDetails>.Filter.Where(x => x.Status == (filter.IsActive.Equals("Active", StringComparison.InvariantCultureIgnoreCase)));
                var AllActivity = await _dbContext.BasicLoanDetails.Find(Activityfilter).ToListAsync();

                AllActivity.ForEach(x => Activelist.Add(new LoanActivityDTO
                {
                    LoanId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    LoanStatus = x.Status ? "Active" : "InActive",
                    CreatedDate = x.CreatedDate,
                    UpdateDate = x.UpdateDate
                }));

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    Activelist = Activelist.Where(x => x.Email.Contains(filter.Email, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(filter.PhoneNumber))
                {
                    Activelist = Activelist.Where(x => x.PhoneNumber.Contains(filter.PhoneNumber, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
                Activelist = Activelist.OrderByDescending(x => x.UpdateDate).ToList();
                if (!Count)
                {
                    Activelist = Activelist.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
                }

            }
            catch (Exception ec)
            {
                return null;
            }
            return Activelist;
        }
    }
}
