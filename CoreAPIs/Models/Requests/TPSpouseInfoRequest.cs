namespace CoreAPIs.Models.Requests
{
    public class TPSpouseInfoRequest
    {
        public SpousePersonalInfo SpousePersonalinfo { get; set; }
        public SpouseAddressInfo SpouseAddressinfo { get; set; }
        public SpouseOtherInfo SpouseOtherinfo { get; set; }
        public class SpousePersonalInfo
        {
            public string TaxPayerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthdate { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class SpouseAddressInfo
        {
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
        }
        public class SpouseOtherInfo
        {
            public string Occupation { get; set; }
            public string FullTimeStudent { get; set; }
            public string PermanentlyDisabled { get; set; }
            public string LegallyBlind { get; set; }
            public string IsIndependent { get; set; }
        }
    }
}
