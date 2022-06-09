using System.Collections.Generic;
using System.Security.Claims;

namespace AuthWebApp.Web.Models
{
    public class UserIdentityDetailsViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Claim> Claims { get; set; }

        public List<string> Roles { get; set; }
    }
}
