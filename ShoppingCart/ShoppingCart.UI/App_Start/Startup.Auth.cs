using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

using Microsoft.Owin.Security.Twitter;
using System.Threading.Tasks;

namespace ShoppingCart.UI
{
    public partial class Startup
    {
        public const string TwitterConsumerKey = "xgiR26tNW8ukf0xbA";
        public const string TwitterConsumerSecret = "eOTtYSwGWyeaabyRyoYbPOTqNz0DOnG8hq0iUBsI";

        public const string FacebookAppId = "467450233371742";
        public const string FacebookAppSecret = "424ccbf5608cef1a68fef8d3fe564de4";
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        public const string TwitterAccessTokenClaimType = "urn:tokens:twitter:accesstoken";
        public const string TwitterAccessTokenSecretClaimType = "urn:tokens:twitter:accesstokensecret";

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");


            var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            {
                AppId = FacebookAppId,
                AppSecret = FacebookAppSecret,
                Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                        foreach (var x in context.User)
                        {
                            var claimType = string.Format("urn:facebook:{0}", x.Key);
                            string claimValue = x.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                            {
                                var userManager = new UserManager<ShoppingCart.Domain.Concrete.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ShoppingCart.Domain.Concrete.ApplicationUser>(new ShoppingCart.Domain.Concrete.ApplicationDbContext()));
                                context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, XmlSchemaString, "Facebook"));
                                string userID = context.Identity.GetUserId();
                                userManager.AddClaimAsync(context.Identity.GetUserId(),new Claim(claimType,claimValue));                                
                                userManager.Dispose();
                            }
                        }
                        return Task.FromResult(0);
                    }
                }

            };
            facebookOptions.Scope.Add("email");
            facebookOptions.Scope.Add("user_birthday");
            facebookOptions.Scope.Add("friends_about_me");
            facebookOptions.Scope.Add("friends_photos");
            //facebookOptions.Scope.Add("friends_about_me");
            app.UseFacebookAuthentication(facebookOptions);







            //app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            //{
            //    ConsumerKey = TwitterConsumerKey,
            //    ConsumerSecret = TwitterConsumerSecret,
            //    Provider = new TwitterAuthenticationProvider()
            //    {
            //        OnAuthenticated = async context =>
            //        {
            //            context.Identity.AddClaim(new Claim(TwitterAccessTokenClaimType, context.AccessToken));
            //            context.Identity.AddClaim(new Claim(TwitterAccessTokenSecretClaimType, context.AccessTokenSecret));
            //        }
            //    }
            //});




            //app.UseFacebookAuthentication(
            //   appId: "467450233371742",
            //   appSecret: "424ccbf5608cef1a68fef8d3fe564de4");

            //app.UseGoogleAuthentication();
        }
    }
}