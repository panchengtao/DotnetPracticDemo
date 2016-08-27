using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Users.Models;

namespace Users.Infrastructure
{
    public class AppRoleManager : RoleManager<IdentityRole>, IDisposable
    {
        public AppRoleManager(RoleStore<IdentityRole> store) : base(store)
        {
        }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options,
            IOwinContext context)
        {
            var db = context.Get<AppIdentityDbContext>();
            var manager = new AppRoleManager(new RoleStore<IdentityRole>(db));

            return manager;
        }
    }
}