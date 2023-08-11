namespace CoreAPIs.Models.Requests
{
    public class TaxPayerInfoRequest
    {
        public PersonalInfo Personalinfo { get; set; }
        public AddressInfo Addressinfo { get; set; }
        public OtherInfo Otherinfo { get; set; }
        public class PersonalInfo
        {
            public string FilingStatusId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthdate { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class AddressInfo
        {
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
        }
        public class OtherInfo
        {
            public string Occupation { get; set; }
            public string FullTimeStudent { get; set; }
            public string PermanentlyDisabled { get; set; }
            public string LegallyBlind { get; set; }
            public string IsIndependent { get; set; }
        }
    }
}
