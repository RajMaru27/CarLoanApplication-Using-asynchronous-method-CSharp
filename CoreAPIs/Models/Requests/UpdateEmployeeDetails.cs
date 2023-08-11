namespace CoreAPIs.Models.Requests
{
    public class UpdateEmployeeDetails
    {
        public UpdatePersonalDetails UpdatePersonaldetails { get; set; }
        public UpdateWorkDetails UpdateWorkdetails { get; set; }
        public UpdateEducationDetails UpdateEducationdetails { get; set; }

        public class UpdatePersonalDetails
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Age { get; set; }
            public DateTime BirthDate { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class UpdateWorkDetails
        {
            public string Department { get; set; }
            public string Designation { get; set; }
            public string WorkingSince { get; set; }
            public string AnnualSalary { get; set; }
        }

        public class UpdateEducationDetails
        {
            public string Qualification { get; set; }
            public string QualificationYear { get; set; }
            public string University { get; set; }
        }
    }
}

