namespace CoreAPIs.Models.Requests
{
    public class TPSpouseInfoUpdate
    {
        public UpdateSpousePersonalInfo UpdateSpousePersonalinfo { get; set; }
        public UpdateSpouseAddressInfo UpdateSpouseAddressinfo { get; set; }
        public UpdateSpouseOtherInfo UpdateSpouseOtherinfo { get; set; }
        public class UpdateSpousePersonalInfo
        {
            public string SpouseId { get; set; }
            public string TaxPayerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthdate { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class UpdateSpouseAddressInfo
        {
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
        }
        public class UpdateSpouseOtherInfo
        {
            public string Occupation { get; set; }
            public string FullTimeStudent { get; set; }
            public string PermanentlyDisabled { get; set; }
            public string LegallyBlind { get; set; }
            public string IsIndependent { get; set; }
        }
    }
}

