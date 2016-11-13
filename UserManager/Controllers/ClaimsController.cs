using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManager.Controllers
{
    using System.Security.Claims;

    using UserManager.Infrastructure;

    public class ClaimsController : Controller
    {
        [Authorize]
        [RequireHttps]
        public ActionResult Index()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
            if (ident == null)
            {
                return this.View("Error", new string[] { "No claims available." });
            }
            else
            {
                return this.View(ident.Claims);
            }
            return View();
        }

        [ClaimsAccess(Issuer = "RemoteClaims", ClaimType = ClaimTypes.PostalCode, Value = "DC 20500")]
        public string OtherAction()
        {
            return "This is the protected action";
        }
    }
}