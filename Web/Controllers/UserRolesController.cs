using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    public class UserRolesController : Controller
    {
        public ActionResult Index()
        {
            var users = Membership.GetAllUsers();
            var membershipUserArray = new MembershipUser[users.Count];
            users.CopyTo(membershipUserArray, 0);
            var membershipUsers = new List<MembershipUser>(membershipUserArray);

            var userAndRoleses = new List<UserAndRoles>();

            foreach (var user in membershipUsers)
            {
                var rolesForUser = Roles.GetRolesForUser(user.UserName);
                userAndRoleses.Add(new UserAndRoles { Email = user.Email, Username = user.UserName, Roles = new List<string>(rolesForUser)});
            }

            return View(userAndRoleses);
        }
    }
}
