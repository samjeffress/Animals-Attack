using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Controllers
{
    public class UserRolesController : Controller
    {
        public ActionResult Index()
        {
            var users = Membership.GetAllUsers();
            return View(users);
        }

    }
}
