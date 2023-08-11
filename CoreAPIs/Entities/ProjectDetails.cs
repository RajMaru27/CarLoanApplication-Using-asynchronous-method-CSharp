namespace CoreAPIs.Entities
{
    public class ProjectDetails : CommonEntityFields
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; }
        public string ClientCompany { get; set; }
        public string ProjectLeader { get; set; }
    }
}
