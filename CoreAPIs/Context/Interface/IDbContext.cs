using CoreAPIs.Entities;
using CoreAPIs.Entities.CarLoanApplication;
using CoreAPIs.Models.Requests;
using MongoDB.Driver;

namespace CoreAPIs.Context.Interface
{
    public interface IDbContext
    {
        MongoClient Client { get; }
        IMongoCollection<Users> Users { get; }
        IMongoCollection<Role> Roles { get; }
        IMongoCollection<Branch> Branches { get; }
        IMongoCollection<ProjectStatus> Projectstatus { get; }
        IMongoCollection<ProjectDetails> Projectdetails { get; }
        IMongoCollection<BasicLoanDetails> BasicLoanDetails { get; }
        IMongoCollection<LoanAddressDetails> LoanAddressDetails { get; }
        IMongoCollection<RequiredLoanDetails> RequiredLoanDetails { get; }
        IMongoCollection<EmploymentDetails> EmploymentDetails { get; }
        IMongoCollection<BankDetails> BankDetails { get; }
        IMongoCollection<EmployeePersonalDetails> EmployeePersonalDetails { get; }
        IMongoCollection<EmployeeWorkDetail> EmployeeWorkDetail { get; }
        IMongoCollection<EmployeeEducationDetails> EmployeeEducationDetails { get; }
        IMongoCollection<TaxPayerFilingStatus> TaxPayerFilingStatus { get; }
        IMongoCollection<TaxPayerPersonalInfo> TaxPayerPersonalInfo { get;}
        IMongoCollection<TaxPayerAddressInfo> TaxPayerAddressInfo { get;}
        IMongoCollection<TaxPayerOtherInfo> TaxPayerOtherInfo { get;}
        IMongoCollection<TPSpounsePersonalInfo> TPSpounsePersonalInfo { get; }
        IMongoCollection<TPSpouseAddressInfo> TPSpouseAddressInfo { get; }
        IMongoCollection<TPSpouseOtherInfo> TPSpouseOtherInfo { get; }
        IMongoCollection<PersonalDetails> PersonalDetails { get; }
        IMongoCollection<AddressDetails> AddressDetails { get; }
        IMongoCollection<FinancialDetails> FinancialDetails { get; }
        IMongoCollection<VehicalDetails> VehicalDetails { get; }
        IMongoCollection<RequestedLoanDetails> RequestedLoanDetails { get; }
    }
}
