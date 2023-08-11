using CoreAPIs.Context.Interface;
using CoreAPIs.Entities;
using CoreAPIs.Entities.CarLoanApplication;
using CoreAPIs.Models.Requests;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace CoreAPIs.Context
{
    public class DBContext : IDbContext
    {
        public DBContext(IConfiguration configuration)
        {
            var _client = new MongoClient(configuration.GetValue<string>("CatalogDatabaseSettings:ConnectionString"));
            var database = _client.GetDatabase(configuration.GetValue<string>("CatalogDatabaseSettings:DatabaseName"));

            Client = _client;
            Users = database.GetCollection<Users>("DemoUser");
            Roles = database.GetCollection<Role>("DemoRole");
            Branches = database.GetCollection<Branch>("DemoBranch");
            Projectstatus = database.GetCollection<ProjectStatus>("Projectstatus");
            Projectdetails = database.GetCollection<ProjectDetails>("ProjectDerail");
            BasicLoanDetails = database.GetCollection<BasicLoanDetails>("BasicLoanDetails");
            LoanAddressDetails = database.GetCollection<LoanAddressDetails>("AddressDetails");
            RequiredLoanDetails = database.GetCollection<RequiredLoanDetails>("RequiredLoanDetails");
            EmploymentDetails = database.GetCollection<EmploymentDetails>("EmploymentDetails");
            BankDetails = database.GetCollection<BankDetails>("BankDetails");
            EmployeePersonalDetails = database.GetCollection<EmployeePersonalDetails>("EmployeePersonalDetails");
            EmployeeWorkDetail = database.GetCollection<EmployeeWorkDetail>("EmployeeWorkDetail");
            EmployeeEducationDetails = database.GetCollection<EmployeeEducationDetails>("EmployeeEducationDetails");
            TaxPayerFilingStatus = database.GetCollection<TaxPayerFilingStatus>("TaxPayerFilingStatus");
            TaxPayerPersonalInfo = database.GetCollection<TaxPayerPersonalInfo>("TaxPayerPersonalInfo");
            TaxPayerAddressInfo = database.GetCollection<TaxPayerAddressInfo>("TaxPayerAddressInfo");
            TaxPayerOtherInfo = database.GetCollection<TaxPayerOtherInfo>("TaxPayerOtherInfo");
            TPSpounsePersonalInfo = database.GetCollection<TPSpounsePersonalInfo>("TPSpounsePersonalInfo");
            TPSpouseAddressInfo = database.GetCollection<TPSpouseAddressInfo>("TPSpouseAddressInfo");
            TPSpouseOtherInfo = database.GetCollection<TPSpouseOtherInfo>("TPSpouseOtherInfo");
            PersonalDetails = database.GetCollection<PersonalDetails>("CarLoanPersonalDetails");
            AddressDetails = database.GetCollection<AddressDetails>("CarLoanAddressDetails");
            FinancialDetails = database.GetCollection<FinancialDetails>("CarLoanFinancialDetails");
            VehicalDetails = database.GetCollection<VehicalDetails>("CarLoanVehicalDetails");
            RequestedLoanDetails = database.GetCollection<RequestedLoanDetails>("CarLoanRequestedLoanDetails");
        }

        public MongoClient Client { get; }
        public IMongoCollection<Users> Users { get; }
        public IMongoCollection<Role> Roles { get; }
        public IMongoCollection<Branch> Branches { get; }
        public IMongoCollection<ProjectStatus> Projectstatus { get; }
        public IMongoCollection<ProjectDetails> Projectdetails { get; }
        public IMongoCollection<BasicLoanDetails> BasicLoanDetails { get; }
        public IMongoCollection<LoanAddressDetails> LoanAddressDetails { get; }
        public IMongoCollection<RequiredLoanDetails> RequiredLoanDetails { get; }
        public IMongoCollection<EmploymentDetails> EmploymentDetails { get; }
        public IMongoCollection<BankDetails> BankDetails { get; }
        public IMongoCollection<EmployeePersonalDetails> EmployeePersonalDetails { get; }
        public IMongoCollection<EmployeeWorkDetail> EmployeeWorkDetail { get; }
        public IMongoCollection<EmployeeEducationDetails> EmployeeEducationDetails { get; }
        public IMongoCollection<TaxPayerFilingStatus> TaxPayerFilingStatus { get; }
        public IMongoCollection<TaxPayerPersonalInfo> TaxPayerPersonalInfo { get; }
        public IMongoCollection<TaxPayerAddressInfo> TaxPayerAddressInfo { get; }
        public IMongoCollection<TaxPayerOtherInfo> TaxPayerOtherInfo { get; }
        public IMongoCollection<TPSpounsePersonalInfo> TPSpounsePersonalInfo { get; }
        public IMongoCollection<TPSpouseAddressInfo> TPSpouseAddressInfo { get; }
        public IMongoCollection<TPSpouseOtherInfo> TPSpouseOtherInfo { get; }
        public IMongoCollection<PersonalDetails> PersonalDetails { get; }
        public IMongoCollection<AddressDetails> AddressDetails { get; }
        public IMongoCollection<FinancialDetails> FinancialDetails { get; }
        public IMongoCollection<VehicalDetails> VehicalDetails { get; }
        public IMongoCollection<RequestedLoanDetails> RequestedLoanDetails { get; }
    }
}
