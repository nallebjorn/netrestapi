using System;

namespace RestDemo.Models
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Role role { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return "object " + username + password;
        }
    }
}