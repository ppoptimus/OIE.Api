using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OIE.Api.Extensions
{
    public static class SecurityExtensions
    {
        //public static void Bind(this BaseEntity entity, ClaimsPrincipal user)
        //{
        //    //if (entity.Id == 0)
        //    //{
        //    //    entity.CreateUserId = (from c in user.Claims select c.Value);
        //    //    entity.CreateDate = DateTime.Now;
        //    //}
        //    entity.ModifyDate = DateTime.Now;
        //}

        public static int GetId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("sub");
            return claim == null ? 0 : Convert.ToInt16(claim.Value);
        }
        public static string GetFullName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim == null ? null : claim.Value;
        }
        public static string GetAddress(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.StreetAddress);
            return claim == null ? null : claim.Value;
        }

    }
}
