using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Infrastructure
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.Google;

    public class GoogleAuthProvider : GoogleOAuth2AuthenticationProvider
    {
        public override void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        public override Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }

        public override Task ReturnEndpoint(GoogleOAuth2ReturnEndpointContext context)
        {
            
            return Task.FromResult<object>(null);
        }
    }
}