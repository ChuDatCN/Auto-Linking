using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
                        backOfficeAuthenticationBuilder.AddOpenIdConnect(
                            // The scheme must be set with this method to work for the back office
                            backOfficeAuthenticationBuilder.SchemeForBackOffice(OpenIdConnectBackOfficeExternalLoginProviderOptions.SchemeName), options =>
                            {
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
                                // map claims
                                options.TokenValidationParameters.NameClaimType = "name";
                                options.TokenValidationParameters.RoleClaimType = "role";

                                options.RequireHttpsMetadata = true;
                                options.GetClaimsFromUserInfoEndpoint = true;
                                options.SaveTokens = true;
                                // add scopes
                                options.Scope.Add("openid");
                                options.Scope.Add("profile");
                                options.Scope.Add("email");
                                //options.Scope.Add("role");

                                options.UsePkce = true;
                                //options.UsePkce = false;
                            });
                    });
            });
            return builder;
        }
    }
}
