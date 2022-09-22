using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;
using System.Security.Claims;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.BackOffice.Security;

namespace Myproject
{
    public static class AddOpenIdConnectAuthentication1
    {
        public static IUmbracoBuilder AddOpenIdConnectAuthentication(this IUmbracoBuilder builder)
        {
            // Register OpenIdConnectBackOfficeExternalLoginProviderOptions here rather than require it in startup
            builder.Services.ConfigureOptions<OpenIdConnectBackOfficeExternalLoginProviderOptions>();

            builder.AddBackOfficeExternalLogins(logins =>
            {
                logins.AddBackOfficeLogin(
                    backOfficeAuthenticationBuilder =>
                    {
#pragma warning disable CS8604 // Possible null reference argument.
                        backOfficeAuthenticationBuilder.AddOpenIdConnect(
                            // The scheme must be set with this method to work for the back office
                            backOfficeAuthenticationBuilder.SchemeForBackOffice(OpenIdConnectBackOfficeExternalLoginProviderOptions.SchemeName),
                            options =>
                            {
                                var config = builder.Config;
                                // use cookies
                                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                // pass configured options along
                                options.Authority = "https://localhost:5001";
                                options.ClientId = "umbraco-backoffice1";
                                options.ClientSecret = "secret";
                                // Use the authorization code flow
                                options.ResponseType = OpenIdConnectResponseType.Code;
                                options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
                                options.CallbackPath = "/signin-oidc";
                                options.SignedOutCallbackPath = "/signout-oidc";
                                // map claims
                                options.TokenValidationParameters.NameClaimType = ClaimTypes.Name;
                                options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
                                options.RequireHttpsMetadata = true;
                                options.GetClaimsFromUserInfoEndpoint = true;
                                options.SaveTokens = true;
                                // add scopes
                                options.Scope.Add("openid");
                                options.Scope.Add("profile");
                                options.Scope.Add("email");
                                options.UsePkce = true;
                                //options.UsePkce = false;

                                options.SignedOutCallbackPath = "https://localhost:5001/Account/Logout";
                                
                                options.SignedOutRedirectUri = "https://localhost:5001/Account/Logout";

                                options.Events.OnRedirectToIdentityProviderForSignOut = async notitfication =>
                                {
                                    var protocolMessage = notitfication.ProtocolMessage;

                                    var logoutUrl = config["OpenIdConnect:LogoutUrl"];
                                    var returnAfterLogout = config["OpenIdConnect:ReturnAfterLogout"];
                                    if (!string.IsNullOrEmpty(logoutUrl) && !string.IsNullOrEmpty(returnAfterLogout))
                                    {
                                        // Some external login providers require an IssuerAddress.
                                        // It requires the logout URL on the external login provider.
                                        // It also need the client_id and a URL which it needs to return to after logout.
                                        protocolMessage.IssuerAddress =
                                            $"{config["OpenIdConnect:LogoutUrl"]}" +
                                            $"?client_id={config["OpenIdConnect:ClientId"]}" +
                                            $"&returnTo={WebUtility.UrlEncode(config["OpenIdConnect:ReturnAfterLogout"])}";
                                    }



                                    await Task.FromResult(0);
                                };
                            });
#pragma warning restore CS8604 // Possible null reference argument.
                    });
            });
            return builder;
        }
    }
}
