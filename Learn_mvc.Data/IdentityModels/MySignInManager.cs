using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.IdentityModels
{
    public class MySignInManager : SignInManager<MyIdentityUser, string>
    {
		public MySignInManager(MyUserManager userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{
		}

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(MyIdentityUser user)
		{
			return user.GenerateUserIdentityAsync((MyUserManager)UserManager);
		}

		public static MySignInManager Create(IdentityFactoryOptions<MySignInManager> options, IOwinContext context)
		{
			return new MySignInManager(context.GetUserManager<MyUserManager>(), context.Authentication);
		}
	}
}
