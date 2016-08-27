﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Users.Models;

namespace Users.Infrastructure
{
    public class AuthorizationServerProvider: OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var userManager = context.OwinContext.GetUserManager<AppUserManager>();

            AppUser user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Account or Password Wrong!");
                return;
            }
            //if (!user.EmailConfirmed)
            //{
            //    context.SetError("invalid_grant", "User did not confirm email.");
            //    return;
            //}
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");
            var ticket = new AuthenticationTicket(oAuthIdentity, null);
            context.Validated(ticket);
        }

    }
}