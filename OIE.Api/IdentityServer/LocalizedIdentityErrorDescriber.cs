using ApplicationCore.Entities;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OIE.Api.IdentityServer
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        //public override IdentityError DefaultError()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "DefaultError",
        //        Description = "En ukjent feil har oppstått."
        //    };
        //}

        //public override IdentityError ConcurrencyFailure()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "ConcurrencyFailure",
        //        Description = "Optimistisk samtidighet feilet, objektet har blitt endret."
        //    };
        //}

        public override IdentityError PasswordMismatch()
        {
           return new IdentityError()
           {
               Code = "PasswordMismatch",
               Description = "รหัสผ่านเดิมไม่ถูกต้อง"
           };
        }

        //public override IdentityError InvalidToken()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "InvalidToken",
        //        Description = "Feil token."
        //    };
        //}

        //public override IdentityError LoginAlreadyAssociated()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "LoginAlreadyAssociated",
        //        Description = "En bruker med dette brukernavnet finnes allerede."
        //    };
        //}

        //public override IdentityError InvalidUserName(string userName)
        //{
        //    IdentityError identityError = new IdentityError();
        //    identityError.Code = "InvalidUserName";
        //    string str = $"Brkernavnet '{userName}' er ikke gyldig. Det kan kun inneholde bokstaver og tall.";
        //    identityError.Description = str;
        //    return identityError;
        //}

        //public override IdentityError InvalidEmail(string email)
        //{
        //    IdentityError identityError = new IdentityError();
        //    identityError.Code = "InvalidEmail";
        //    string str = $"E-post '{email}' er ugyldig.";
        //    identityError.Description = str;
        //    return identityError;
        //}

        public override IdentityError DuplicateUserName(string userName)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "DuplicateUserName";
            string str = $"ชื่อบัญชีผู้ใช้ '{userName}' ถูกใช้งานแล้ว";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "DuplicateEmail";
            string str = $"อีเมล '{email}' ถูกใช้งานแล้ว";
            identityError.Description = str;
            return identityError;
        }

        //public override IdentityError InvalidRoleName(string role)
        //{
        //    IdentityError identityError = new IdentityError();
        //    identityError.Code = "InvalidRoleName";
        //    string str = $"Rollenavn '{role}' er ugyldig.";
        //    identityError.Description = str;
        //    return identityError;
        //}

        //public override IdentityError DuplicateRoleName(string role)
        //{
        //    IdentityError identityError = new IdentityError();
        //    identityError.Code = "DuplicateRoleName";
        //    string str = $"Rollenavn '{role}' er allerede tatt.";
        //    identityError.Description = str;
        //    return identityError;
        //}

        //public virtual IdentityError UserAlreadyHasPassword()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "UserAlreadyHasPassword",
        //        Description = "Bruker har allerede passord satt."
        //    };
        //}

        //public override IdentityError UserLockoutNotEnabled()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "UserLockoutNotEnabled",
        //        Description = "Utestenging er ikke slått på for denne brukeren."
        //    };
        //}

        //public override IdentityError UserAlreadyInRole(string role)
        //{
        //    IdentityError identityError = new IdentityError();
        //    identityError.Code = "UserAlreadyInRole";
        //    string str = $"Brukeren er allerede i rolle '{role}'.";
        //    identityError.Description = str;
        //    return identityError;
        //}

        //public override IdentityError UserNotInRole(string role)
        //{
        //    IdentityError identityError = new IdentityError();
        //    identityError.Code = "UserNotInRole";
        //    string str = $"Bruker er ikke i rolle '{role}'.";
        //    identityError.Description = str;
        //    return identityError;
        //}

        public override IdentityError PasswordTooShort(int length)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "PasswordTooShort";
            string str = $"กรุณาป้อนอักขระอย่างน้อย {length} ตัวให้ผสมกันทั้งตัวเลข ตัวอักษร";
            identityError.Description = str;
            return identityError;
        }

        //public override IdentityError PasswordRequiresNonAlphanumeric()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "PasswordRequiresNonAlphanumeric",
        //        Description = "Passordet må inneholde minst ett spesialtegn."
        //    };
        //}

        //public override IdentityError PasswordRequiresDigit()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "PasswordRequiresDigit",
        //        Description = "Passordet må inneholde minst ett tall."
        //    };
        //}

        //public override IdentityError PasswordRequiresLower()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "PasswordRequiresLower",
        //        Description = "Passordet må inneholde minst en liten bokstav (a-z)."
        //    };
        //}

        //public override IdentityError PasswordRequiresUpper()
        //{
        //    return new IdentityError()
        //    {
        //        Code = "PasswordRequiresUpper",
        //        Description = "Passordet må inneholde minst en stor bokstav (A-Z)."
        //    };
        //}
    }
}