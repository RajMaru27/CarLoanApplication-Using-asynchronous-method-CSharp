using CoreAPIs.Context.Interface;
using CoreAPIs.Entities.CarLoanApplication;
using CoreAPIs.Models.DTO;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CoreAPIs.Repository.Service
{
    public class CarLoanApplicationRep : ICarLoanApplicationRep
    {
        private readonly IDbContext _context;
        public CarLoanApplicationRep(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GeneralModel> AddAsync(CarLoanAppRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<PersonalDetails>.Filter.Where(x => x.Email == request.Personadetail.Email);
                var details = await _context.PersonalDetails.Find(filter).FirstOrDefaultAsync();
                if (details == null)
                {
                    PersonalDetails personalDetails = new PersonalDetails();
                    personalDetails = new PersonalDetails
                    {
                        FirstName = request.Personadetail.FirstName,
                        LastName = request.Personadetail.LastName,
                        BirthDate = request.Personadetail.BirthDate,
                        MaritalStatus = request.Personadetail.MaritalStatus,
                        Email = request.Personadetail.Email,
                        PhoneNumber = request.Personadetail.PhoneNumber,
                        EmergencyContactNumber = request.Personadetail.EmergencyContactNumber,
                        DriversLicenseNumber = request.Personadetail.DriversLicenseNumber,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Status = true
                    };
                    await _context.PersonalDetails.InsertOneAsync(personalDetails);

                    AddressDetails addressDetails = new AddressDetails();
                    addressDetails = new AddressDetails
                    {
                        LoanId = personalDetails.Id,
                        AddressLine1 = request.Addressdetail.AddressLine1,
                        AddressLine2 = request.Addressdetail.AddressLine2,
                        City = request.Addressdetail.City,
                        State = request.Addressdetail.State,
                        PostalCode = request.Addressdetail.PostalCode,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _context.AddressDetails.InsertOneAsync(addressDetails);

                    FinancialDetails financialDetails = new FinancialDetails();
                    financialDetails = new FinancialDetails
                    {
                        LoanId = personalDetails.Id,
                        BankName = request.Financialdetail.BankName,
                        Branch = request.Financialdetail.Branch,
                        AccountType = request.Financialdetail.AccountType,
                        AccountNumber = request.Financialdetail.AccountNumber,
                        OwnOrRent = request.Financialdetail.OwnOrRent,
                        CurrentLoan = request.Financialdetail.CurrentLoan,
                        WorkingStatus = request.Financialdetail.WorkingStatus,
                        MonthlyIncome = request.Financialdetail.MonthlyIncome,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _context.FinancialDetails.InsertOneAsync(financialDetails);

                    VehicalDetails vehicalDetails = new VehicalDetails();
                    vehicalDetails = new VehicalDetails
                    {
                        LoanId = personalDetails.Id,
                        MakeAndModel = request.Vehicaldetail.MakeAndModel,
                        Variant = request.Vehicaldetail.Variant,
                        RegisteredDate = request.Vehicaldetail.RegisteredDate,
                        Mileage = request.Vehicaldetail.Mileage,
                        Insurance = request.Vehicaldetail.Insurance,
                        RegistrationNumber = request.Vehicaldetail.RegistrationNumber,
                        NEWorSECONDHAND = request.Vehicaldetail.NEWorSECONDHAND,
                        HPI = request.Vehicaldetail.HPI,
                        FullPrice = request.Vehicaldetail.FullPrice,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _context.VehicalDetails.InsertOneAsync(vehicalDetails);

                    RequestedLoanDetails requestedLoanDetails = new RequestedLoanDetails();
                    requestedLoanDetails = new RequestedLoanDetails
                    {
                        LoanId = personalDetails.Id,
                        CarType = request.Requestedloandetail.CarType,
                        LoanAmount = request.Requestedloandetail.LoanAmount,
                        Terms = request.Requestedloandetail.Terms,
                        PrefferedPayment = request.Requestedloandetail.PrefferedPayment,
                        CoSigner = request.Requestedloandetail.CoSigner,
                        CoSignerName = request.Requestedloandetail.CoSignerName,
                        CoSignerPhoneNo = request.Requestedloandetail.CoSignerPhoneNo,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };
                    await _context.RequestedLoanDetails.InsertOneAsync(requestedLoanDetails);

                    model.Id = personalDetails.Id;
                    model.Status = true;
                    model.Message = "Data created successfully";
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<GeneralModel> UpdateAsync(CarLoanAppUpdate request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<PersonalDetails>.Filter.Where(x => x.Id == request.UpdatePersonadetail.LoanId);
                var updatepersonaldetails = Builders<PersonalDetails>.Update;
                var personaldetail = updatepersonaldetails.Combine(new UpdateDefinition<PersonalDetails>[]
                {
                    updatepersonaldetails.Set(x => x.FirstName,request.UpdatePersonadetail.FirstName),
                    updatepersonaldetails.Set(x => x.LastName,request.UpdatePersonadetail.LastName),
                    updatepersonaldetails.Set(x => x.BirthDate,request.UpdatePersonadetail.BirthDate),
                    updatepersonaldetails.Set(x => x.MaritalStatus,request.UpdatePersonadetail.MaritalStatus),
                    updatepersonaldetails.Set(x => x.Email,request.UpdatePersonadetail.Email),
                    updatepersonaldetails.Set(x => x.PhoneNumber,request.UpdatePersonadetail.PhoneNumber),
                    updatepersonaldetails.Set(x => x.EmergencyContactNumber,request.UpdatePersonadetail.EmergencyContactNumber),
                    updatepersonaldetails.Set(x => x.DriversLicenseNumber, request.UpdatePersonadetail.DriversLicenseNumber),
                    updatepersonaldetails.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.PersonalDetails.FindOneAndUpdateAsync(filter, personaldetail);

                var addressfilter = Builders<AddressDetails>.Filter.Where(x => x.LoanId == request.UpdatePersonadetail.LoanId);
                var updateaddress = Builders<AddressDetails>.Update;
                var addressdetails = updateaddress.Combine(new UpdateDefinition<AddressDetails>[]
                {
                    updateaddress.Set(x => x.AddressLine1,request.UpdateAddressdetail.AddressLine1),
                    updateaddress.Set(x => x.AddressLine2,request.UpdateAddressdetail.AddressLine2),
                    updateaddress.Set(x => x.City,request.UpdateAddressdetail.City),
                    updateaddress.Set(x => x.State,request.UpdateAddressdetail.State),
                    updateaddress.Set(x => x.PostalCode,request.UpdateAddressdetail.PostalCode),
                    updateaddress.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.AddressDetails.FindOneAndUpdateAsync(addressfilter, addressdetails);

                var FDfilter = Builders<FinancialDetails>.Filter.Where(x => x.LoanId == request.UpdatePersonadetail.LoanId);
                var updateFD = Builders<FinancialDetails>.Update;
                var Financialdetails = updateFD.Combine(new UpdateDefinition<FinancialDetails>[]
                {
                    updateFD.Set(x => x.BankName,request.UpdateFinancialdetail.BankName),
                    updateFD.Set(x => x.Branch,request.UpdateFinancialdetail.Branch),
                    updateFD.Set(x => x.AccountType,request.UpdateFinancialdetail.AccountType),
                    updateFD.Set(x => x.AccountNumber,request.UpdateFinancialdetail.AccountNumber),
                    updateFD.Set(x => x.OwnOrRent,request.UpdateFinancialdetail.OwnOrRent),
                    updateFD.Set(x => x.CurrentLoan,request.UpdateFinancialdetail.CurrentLoan),
                    updateFD.Set(x => x.WorkingStatus,request.UpdateFinancialdetail.WorkingStatus),
                    updateFD.Set(x => x.MonthlyIncome,request.UpdateFinancialdetail.MonthlyIncome),
                    updateFD.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.FinancialDetails.FindOneAndUpdateAsync(FDfilter, Financialdetails);

                var vehicalfilter = Builders<VehicalDetails>.Filter.Where(x => x.LoanId == request.UpdatePersonadetail.LoanId);
                var updatevehical = Builders<VehicalDetails>.Update;
                var vehicaldetails = updatevehical.Combine(new UpdateDefinition<VehicalDetails>[]
                {
                    updatevehical.Set(x => x.MakeAndModel,request.UpdateVehicaldetail.MakeAndModel),
                    updatevehical.Set(x => x.Variant,request.UpdateVehicaldetail.Variant),
                    updatevehical.Set(x => x.RegisteredDate,request.UpdateVehicaldetail.RegisteredDate),
                    updatevehical.Set(x => x.Mileage,request.UpdateVehicaldetail.Mileage),
                    updatevehical.Set(x => x.Insurance,request.UpdateVehicaldetail.Insurance),
                    updatevehical.Set(x => x.RegistrationNumber,request.UpdateVehicaldetail.RegistrationNumber),
                    updatevehical.Set(x => x.NEWorSECONDHAND,request.UpdateVehicaldetail.NEWorSECONDHAND),
                    updatevehical.Set(x => x.HPI,request.UpdateVehicaldetail.HPI),
                    updatevehical.Set(x => x.FullPrice,request.UpdateVehicaldetail.FullPrice),
                    updatevehical.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.VehicalDetails.FindOneAndUpdateAsync(vehicalfilter, vehicaldetails);

                var RLDfilter = Builders<RequestedLoanDetails>.Filter.Where(x => x.LoanId == request.UpdatePersonadetail.LoanId);
                var updateRLD = Builders<RequestedLoanDetails>.Update;
                var RLdetails = updateRLD.Combine(new UpdateDefinition<RequestedLoanDetails>[]
                {
                    updateRLD.Set(x => x.CarType,request.UpdateRequestedloandetail.CarType),
                    updateRLD.Set(x => x.LoanAmount,request.UpdateRequestedloandetail.LoanAmount),
                    updateRLD.Set(x => x.Terms,request.UpdateRequestedloandetail.Terms),
                    updateRLD.Set(x => x.PrefferedPayment,request.UpdateRequestedloandetail.PrefferedPayment),
                    updateRLD.Set(x => x.CoSigner,request.UpdateRequestedloandetail.CoSigner),
                    updateRLD.Set(x => x.CoSignerName,request.UpdateRequestedloandetail.CoSignerName),
                    updateRLD.Set(x => x.CoSignerPhoneNo,request.UpdateRequestedloandetail.CoSignerPhoneNo),
                    updateRLD.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.RequestedLoanDetails.FindOneAndUpdateAsync(RLDfilter, RLdetails);

                model.Id = request.UpdatePersonadetail.LoanId;
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

        public async Task<IEnumerable<LoanApplicantList>> GetAll()
        {
            List<LoanApplicantList> applicantLists = new List<LoanApplicantList>();

            applicantLists = (from PD in _context.PersonalDetails.Find(x => !string.IsNullOrEmpty(x.Id)).ToList()
                              join RLD in _context.RequestedLoanDetails.Find(x => !string.IsNullOrEmpty(x.LoanId)).ToList()
                              on PD.Id equals RLD.LoanId
                              select new LoanApplicantList
                              {
                                  LoanId = PD.Id,
                                  FirstName = PD.FirstName,
                                  LastName = PD.LastName,
                                  Email = PD.Email,
                                  Birthdate = PD.BirthDate,
                                  MaritalStatus = PD.MaritalStatus,
                                  Status = PD.Status,
                                  LoanType = RLD.CarType,
                                  LoanAmount = RLD.LoanAmount
                              }).ToList();
            return applicantLists;
        }

        public async Task<GeneralModel> Delete(LoanApplicantStatus request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<PersonalDetails>.Filter.Where(x => x.Id == request.LoanId);
                var updatedetails = Builders<PersonalDetails>.Update;
                var details = updatedetails.Combine(new UpdateDefinition<PersonalDetails>[]
                {
                    updatedetails.Set(x => x.Status,false),
                    updatedetails.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.PersonalDetails.FindOneAndUpdateAsync(filter, details);

                model.Id = request.LoanId;
                model.Status = true;
                model.Message = "LoanStatus is InActive";
            }
            catch (Exception e)
            {
                model.Status = false;
                model.Message = e.Message;
            }
            return model;
        }

        public async Task<GeneralModel> Restore(LoanApplicantStatus request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                var filter = Builders<PersonalDetails>.Filter.Where(x => x.Id == request.LoanId);
                var update = Builders<PersonalDetails>.Update;
                var details = update.Combine(new UpdateDefinition<PersonalDetails>[]
                {
                    update.Set(x => x.Status,true),
                    update.Set(x => x.UpdateDate,DateTime.UtcNow)
                });
                await _context.PersonalDetails.FindOneAndUpdateAsync(filter, details);

                model.Id = request.LoanId;
                model.Status = true;
                model.Message = "LoanStatus is Active";
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public async Task<List<CarLoanSearchDetails>> FilterActivity(CarLoanSearchList filter)
        {
            List<CarLoanSearchDetails> Filterlist = new List<CarLoanSearchDetails>();
            try
            {
                Filterlist = (from PD in _context.PersonalDetails.Find(x => x.Email == filter.data
                                                                         || x.FirstName == filter.data
                                                                         || x.LastName == filter.data
                                                                         || x.PhoneNumber == filter.data).ToList()
                              join RLD in _context.RequestedLoanDetails.Find(x => !string.IsNullOrEmpty(x.LoanId)).ToList()
                              on PD.Id equals RLD.LoanId
                              select new CarLoanSearchDetails
                              {
                                  LoanId = PD.Id,
                                  FirstName = PD.FirstName,
                                  LastName = PD.LastName,                              
                                  BirthDate = PD.BirthDate,
                                  MaritalStatus = PD.MaritalStatus,
                                  Email = PD.Email,
                                  ActivityStatus = PD.Status,
                                  LoanType = RLD.CarType,
                                  LoanAmount = RLD.LoanAmount
                              }).ToList();
            }
            catch (Exception x)
            {
                return null;
            }
            return Filterlist;
        }

        public async Task<CarLoanApplicantDetails> Details(string LoanId)
        {
            CarLoanApplicantDetails Details = new CarLoanApplicantDetails();
            try
            {
                var filter = Builders<PersonalDetails>.Filter.Where(x => x.Id ==  LoanId);
                var data = await _context.PersonalDetails.Find(filter).FirstOrDefaultAsync();

                var Addressfilter = Builders<AddressDetails>.Filter.Where(x => x.LoanId == LoanId);
                var addressdata = await _context.AddressDetails.Find(Addressfilter).FirstOrDefaultAsync();

                var Financialfilter = Builders<FinancialDetails>.Filter.Where(x => x.LoanId == LoanId);
                var Financialdata = await _context.FinancialDetails.Find(Financialfilter).FirstOrDefaultAsync();

                var Vehicalfilter = Builders<VehicalDetails>.Filter.Where(x => x.LoanId == LoanId);
                var Vehicaldata = await _context.VehicalDetails.Find(Vehicalfilter).FirstOrDefaultAsync();

                var ReqLoanfilter = Builders<RequestedLoanDetails>.Filter.Where(x => x.LoanId == LoanId);
                var ReqLoandata = await _context.RequestedLoanDetails.Find(ReqLoanfilter).FirstOrDefaultAsync();

                Details.LoanId = data.Id;
                Details.FirstName = data.FirstName;
                Details.LastName = data.LastName;
                Details.BirthDate = data.BirthDate;
                Details.MaritalStatus = data.MaritalStatus;
                Details.Email = data.Email;
                Details.PhoneNumber = data.PhoneNumber;
                Details.EmergencyContactNumber = data.EmergencyContactNumber;
                Details.DriversLicenseNumber = data.DriversLicenseNumber;
                Details.Status = data.Status;
                Details.AddressLine1 = addressdata.AddressLine1;
                Details.AddressLine2 = addressdata.AddressLine2;
                Details.City = addressdata.City;
                Details.State = addressdata.State;
                Details.Postalcode = addressdata.PostalCode;
                Details.BankName = Financialdata.BankName;
                Details.Branch = Financialdata.Branch;
                Details.AccountType = Financialdata.AccountType;
                Details.AccountNumber = Financialdata.AccountNumber;
                Details.OwnOrRent = Financialdata.OwnOrRent;
                Details.CurrentLoan = Financialdata.CurrentLoan;
                Details.WorkingStatus = Financialdata.WorkingStatus;
                Details.MonthlyIncome = Financialdata.MonthlyIncome;
                Details.MakeAndModel = Vehicaldata.MakeAndModel;
                Details.Variant = Vehicaldata.Variant;
                Details.RegisteredDate = Vehicaldata.RegisteredDate;
                Details.Mileage = Vehicaldata.Mileage;
                Details.Insurance = Vehicaldata.Insurance;
                Details.RegistrationNumber = Vehicaldata.RegistrationNumber;
                Details.NEWorSECONDHAND = Vehicaldata.NEWorSECONDHAND;
                Details.HPI = Vehicaldata.HPI;
                Details.FullPrice = Vehicaldata.FullPrice;
                Details.CarType = ReqLoandata.CarType;
                Details.LoanAmount = ReqLoandata.LoanAmount;
                Details.Terms = ReqLoandata.Terms;
                Details.PrefferedPayment = ReqLoandata.PrefferedPayment;
                Details.CoSigner = ReqLoandata.CoSigner;
                Details.CoSignerName = ReqLoandata.CoSignerName;
                Details.CoSignerPhoneNo = ReqLoandata.CoSignerPhoneNo;
                Details.CreatedDate = data.CreatedDate;
                Details.UpdateDate = data.UpdateDate;
            }
            catch (Exception)
            {
                return null;
            }
            return Details;
        }

        public async Task<List<LoanApplicantList>> InActiveListOnly()
        {
            List<LoanApplicantList> details = new List<LoanApplicantList>();
            try
            {
                details = (from PD in _context.PersonalDetails.Find(x => !string.IsNullOrEmpty(x.Id) && x.Status == false).ToList()
                           join RLD in _context.RequestedLoanDetails.Find(x => !string.IsNullOrEmpty(x.LoanId)).ToList()
                           on PD.Id equals RLD.LoanId
                           select new LoanApplicantList
                           {
                               LoanId = PD.Id,
                               FirstName = PD.FirstName,
                               LastName = PD.LastName,
                               Birthdate = PD.BirthDate,
                               MaritalStatus = PD.MaritalStatus,
                               Email = PD.Email,
                               LoanType = RLD.CarType,
                               LoanAmount = RLD.LoanAmount,
                               Status = PD.Status
                           }).ToList();                           
            }
            catch
            {
                return null;
            }
            return details;
        }
    }
}
