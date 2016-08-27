using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Users.Infrastructure;

namespace Users.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountApiController:ApiController
    {
        //  创建指定的AppUserManager类
        private AppUserManager AppUserManager => Request.GetOwinContext().GetUserManager<AppUserManager>();
        
        [Authorize]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);
            if (user != null)
            {
                return Ok(user);
            }
            
            return NotFound();
        }
    }
}