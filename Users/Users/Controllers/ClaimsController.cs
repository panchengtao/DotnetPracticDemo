using System.Security.Claims;
using System.Web.Mvc;

namespace Users.Controllers
{
    public class ClaimsController : Controller
    {
        // GET: Claims
        [Authorize]
        public ActionResult Index()
        {
            var ident = HttpContext.User.Identity as ClaimsIdentity;
            if (ident == null)
            {
                return View("Error", new[] {"No claims available"});
            }
            return View(ident.Claims);
        }

        [Authorize(Roles = "DCStaff")]
        public string OtherAction()
        {
            return "This is the protected action";
        }
    }
}