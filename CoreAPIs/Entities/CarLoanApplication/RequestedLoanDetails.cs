namespace CoreAPIs.Entities.CarLoanApplication
{
    public class RequestedLoanDetails : CommonEntityFields
    {
        public string LoanId { get; set; }
        public string CarType { get; set; }
        public string LoanAmount { get; set; }
        public string Terms { get; set; }
        public string PrefferedPayment { get; set; }
        public string CoSigner { get; set; }
        public string CoSignerName { get; set; }
        public string CoSignerPhoneNo { get; set;}
    }
}
