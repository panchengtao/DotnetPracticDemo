using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

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

        // additional properties will go here
    }
}