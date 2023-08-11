using System.ComponentModel.DataAnnotations;

namespace CoreAPIs.Models.Requests
{
    public class CarLoanAppRequest
    {
        public PersonalDetail Personadetail { get; set; }
        public AddressDetail Addressdetail { get; set; }
        public FinancialDetail Financialdetail { get; set; }
        public VehicalDetail Vehicaldetail { get; set; }
        public RequestedLoanDetail Requestedloandetail { get; set; }

        public class PersonalDetail
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string MaritalStatus { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string EmergencyContactNumber { get; set; }
            public string DriversLicenseNumber { get; set; }
        }
        public class AddressDetail
        {
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
        }
        public class FinancialDetail
        {
            public string BankName { get; set; }
            public string Branch { get; set; }
            public string AccountType { get; set; }
            public string AccountNumber { get; set; }
            public string OwnOrRent { get; set; }
            public string CurrentLoan { get; set; }
            public string WorkingStatus { get; set; }
            public string MonthlyIncome { get; set; }
        }
        public class VehicalDetail
        {
            public string MakeAndModel { get; set; }
            public string Variant { get; set; }
            public DateTime RegisteredDate { get; set; }
            public string Mileage { get; set; }
            public string Insurance { get; set; }
            public string RegistrationNumber { get; set; }
            public string NEWorSECONDHAND { get; set; }
            public string HPI { get; set; }
            public string FullPrice { get; set; }
        }
        public class RequestedLoanDetail
        {
            public string CarType { get; set; }
            public string LoanAmount { get; set; }
            public string Terms { get; set; }
            public string PrefferedPayment { get; set; }
            public string CoSigner { get; set; }
            public string CoSignerName { get; set; }
            public string CoSignerPhoneNo { get; set; }
        }
    }
}
