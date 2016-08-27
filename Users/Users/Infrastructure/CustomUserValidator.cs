using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Users.Models;

namespace Users.Infrastructure
{
    public class CustomUserValidator: UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser item)
        {
            IdentityResult result =await base.ValidateAsync(item);

            if (!item.Email.ToLower().EndsWith("@example.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only example.com email addresses are allowed");
                result = new IdentityResult(errors);
            }

            return result;
        }


    }
}