using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManager.Infrastructure
{
    using System.Security.Claims;

    public class ClaimsRoles
    {
        public static IEnumerable<Claim> CreateRolesFromClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
            if (user.HasClaim(x => x.Type == ClaimTypes.StateOrProvince && x.Issuer == "RomteClaims" && x.Value == "DC")
                && user.HasClaim(x=>x.Type==ClaimTypes.Role && x.Value =="Employees"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "DCStaff"));
            }
            return claims;
        }
    }
}