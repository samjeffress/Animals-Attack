using System.Collections.Generic;

namespace Web.Models
{
    public class UserAndRoles
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}