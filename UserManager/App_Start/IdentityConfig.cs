using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;

    using Owin;

    using UserManager.Infrastructure;
    using Microsoft.Owin.Security.Google;

    using UserManager.Models;

    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AppIdentityDbContext>(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            //app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                            {
                                                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                                LoginPath = new PathString("/Account/Login")
                                            });

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "472564029499-h1cgk73nprj7ia2jum2o21ioctqj4mnj.apps.googleusercontent.com",
                ClientSecret = "tdOSJSTaqxNYlXyJ7UKsIOEK"
            };
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }
    }
}