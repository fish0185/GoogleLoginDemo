﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Infrastructure
{
    using System.Security.Claims;
    using System.Web.Mvc;
    public class ClaimsAccessAttribute : AuthorizeAttribute
    {
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }

        protected override bool AuthorizeCore(HttpContextBase context)
        {
            return context.User.Identity.IsAuthenticated && context.User.Identity is ClaimsIdentity
                   && ((ClaimsIdentity)context.User.Identity).HasClaim(
                       x => x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value);
        }
    }
}