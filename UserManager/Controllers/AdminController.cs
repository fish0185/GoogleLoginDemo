using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManager.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using UserManager.Infrastructure;
    using UserManager.Models;

    [RequireHttps]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser {UserName = model.Name, Email = model.Email};
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return this.View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail
                = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass
                    = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                        UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded
                && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return this.View("Error", result.Errors);
                }
            }
            else
            {
                return this.View("Error", new string[] { "User Not Found" });
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}