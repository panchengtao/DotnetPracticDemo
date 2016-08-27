using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Users.Infrastructure;

namespace Users.Models
{
    public enum Cities
    {
        London, Paris, Chicago
    }

    public enum Countries
    {
        None, Uk, France, Usa
    }

    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }

        public Countries Country { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.London:
                    Country = Countries.Uk;
                    break;
                case Cities.Paris:
                    Country = Countries.France;
                    break;
                case Cities.Chicago:
                    Country = Countries.Usa;
                    break;
                default:
                    Country = Countries.None;
                    break;
            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        // additional properties will go here
    }
}