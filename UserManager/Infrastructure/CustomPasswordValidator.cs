using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Infrastructure
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string pass)
        {
            IdentityResult result = await base.ValidateAsync(pass);
            if (pass.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Passwords cannot contain numeric sequences");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}