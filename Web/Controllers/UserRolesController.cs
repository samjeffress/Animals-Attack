using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Helpers;
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

        public ActionResult Edit(string userName)
        {
            var membershipUser = Membership.GetUser(userName);
            if (membershipUser == null)
                throw new ArgumentException(userName + " is not a user.");
            var rolesForUser = Roles.GetRolesForUser(userName);
            var userAndRoles = new UserAndRoles
                                   {
                                       Username = userName, 
                                       Email = membershipUser.Email, 
                                       Roles = new List<string>(rolesForUser)
                                   };
            RoleOptionsViewBag(userAndRoles.Roles);
            return View("Edit", userAndRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            var updatedUserRoles = new UserAndRoles();
            UpdateModel(updatedUserRoles);
            if (User.IsInRole(UserRoles.MasterOfTheEverything.ToString()))
            {
                var membershipUser = Membership.GetUser(updatedUserRoles.Username);
                if (membershipUser == null)
                    throw  new ArgumentException(updatedUserRoles.Username + " is not a user.");
                var currentRolesForUser = new List<string>(Roles.GetRolesForUser(updatedUserRoles.Username));
                
                // only updating roles, not updating username / email
                foreach (object role in Enum.GetValues(typeof(UserRoles)))
                {
                    if (updatedUserRoles.Roles.Contains(role.ToString()) && !currentRolesForUser.Contains(role.ToString()))
                    {
                        if (!Roles.RoleExists(role.ToString()))
                            Roles.CreateRole(role.ToString());
                        Roles.AddUserToRole(updatedUserRoles.Username, role.ToString());
                    }
                    else if (!updatedUserRoles.Roles.Contains(role.ToString()) && currentRolesForUser.Contains(role.ToString()))
                    {
                        Roles.RemoveUserFromRole(updatedUserRoles.Username, role.ToString());
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "You are not able to update users without being master of everything. Sorry.";
            return View("Error");
        }

        private void RoleOptionsViewBag(List<string> selectedItems)
        {
            var possibleRoles = Enum.GetValues(typeof(UserRoles));

            var locationList = selectedItems == null ? new MultiSelectList(possibleRoles) : new MultiSelectList(possibleRoles, selectedItems);
            ViewBag.RoleOptions = locationList;
        }

        
    }
}
