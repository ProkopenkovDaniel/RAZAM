using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace RAZAM.Models
{
    public class RazamUserManager : UserManager<RazamUser>
    {
        public RazamUserManager(IUserStore<RazamUser> store) : base(store) { }
        public static RazamUserManager Create(IdentityFactoryOptions<RazamUserManager> options,
            IOwinContext context)
        {
            RazamContext db = context.Get<RazamContext>();
            RazamUserManager manager = new RazamUserManager(new UserStore<RazamUser>(db));
            return manager;
        }
    }
}