namespace CoreAPIs.Models.Requests
{
    public class EmployeelDetailRequest
    {
        public PersonalDetails Personaldetails { get; set; }
        public WorkDetails Workdetails { get; set; }
        public EducationDetails Educationdetails { get; set; }

        public class PersonalDetails
        {
            public string Name { get; set; }
            public string Age { get; set; }
            public DateTime BirthDate { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class WorkDetails
        {
            public string Department { get; set; }
            public string Designation { get; set; }
            public string WorkingSince { get; set; }
            public string AnnualSalary { get; set; }
        }

        public class EducationDetails
        {
            public string Qualification { get; set; }
            public string QualificationYear { get; set; }
            public string University { get; set; }
        }
    }
}
