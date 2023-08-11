namespace CoreAPIs.Entities.CarLoanApplication
{
    public class VehicalDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        public string MakeAndModel { get; set; }
        public string Variant { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Mileage { get;set; }
        public string Insurance { get; set; }
        public string RegistrationNumber { get; set; }
        public string NEWorSECONDHAND { get; set; }
        public string HPI { get;set; }
        public string FullPrice { get; set; }
    }
}
