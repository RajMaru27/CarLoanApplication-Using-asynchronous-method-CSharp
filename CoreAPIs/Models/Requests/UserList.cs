﻿namespace CoreAPIs.Models.Requests
{
    public class UserList
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
    }
}
