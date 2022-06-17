using CoffeeShop.Common;
using CoffeeShop.Data;
using CoffeeShop.Models.Models;
using CoffeeShop.Web.Infrastucture.Core;

using Evernote.EDAM.Type;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;

using Owin;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeShop.Web.App_Start
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit
        // https://go.microsoft.com/fwlink/?LinkId=301864 For more information
        // on configuring authentication, please visit
        // http://go.microsoft.com/fwlink/?LinkId=301864

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use
            // a single instance per request
            app.CreatePerOwinContext(CoffeeShopDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateManager);
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var authCookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(30),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.


                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user
                            .GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie))
                },


                // This line to force use SystemWebCookieManager for OWIN
                CookieManager = new SystemWebCookieManager()

            };

            app.UseCookieAuthentication(authCookieOptions);

            app.UseExternalSignInCookie(
            DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third
            // party login providers app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            // app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //ConfigHelper Facebook Authentication Options

            var facebookAuthenticationOptions = new FacebookAuthenticationOptions
            {
                AppId = ConfigHelper.GetByKey(CommonConstants.FacebookAppId),
                AppSecret = ConfigHelper.GetByKey(CommonConstants.FacebookAppSecret),
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
            };

            facebookAuthenticationOptions.Scope.Add("email");

            app.UseFacebookAuthentication(facebookAuthenticationOptions);


            //Config Google Authentication Options
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = ConfigHelper.GetByKey(CommonConstants.GoogleClientId),
                ClientSecret = ConfigHelper.GetByKey(CommonConstants.GoogleClientSecret)
            });
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(
            OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null)
                    allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();

                ApplicationUser user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }

                if (user != null)
                {
                    if (user.AdminAccessPermission)
                    {
                        ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "User do not have permission to access the following resources: AdminControlPanel");
                        context.Rejected();
                    }


                }
                else
                {
                    context.SetError("invalid_grant", "User Name or Password incorrect.'");
                    context.Rejected();
                }
            }
        }

        private static UserManager<ApplicationUser> CreateManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<ApplicationUser>(
            context.Get<CoffeeShopDbContext>());
            var owinManager = new UserManager<ApplicationUser>(userStore);
            return owinManager;
        }
    }
}