using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Myproject
{
    public class NewHope
    {
        //private static Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedContext ctx)
        //{
        //    // This is where we appear after a successfull login, since we use AADB2C Open ID Connect, we already get a lot with the initial authorization code
        //    ctx.Options.Events.OnAuthorizationCodeReceived = async context =>
        //    {
        //        context.ProtocolMessage.RedirectUri = context.ProtocolMessage.RedirectUri.Replace("http:", "https:");
        //        await Task.FromResult(0);
        //    };

        //    // Get the current claims
        //    var claims = ctx.Principal?.Claims;

        //    // Find the original JWT token
        //    string jwtTokenFromIdentityProvider =
        //        claims.Where(m => m.Type.Equals("idp_access_token", StringComparison.OrdinalIgnoreCase))
        //        .Select(m => m.Value).FirstOrDefault();

        //    // Resolve the token to a readable format
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(jwtTokenFromIdentityProvider);
        //    var tokenFromIdentityProvider = (JwtSecurityToken)jsonToken;

        //    // Get the principal name (so: the login email)
        //    var principalName = tokenFromIdentityProvider.Claims.First(claim => claim.Type == "upn").Value;

        //    // Define the new claim
        //    var claimsToAdd = new List<Claim>
        //{
        //    new Claim("email", principalName)
        //};

        //    // Add the claim by adding a new ClaimsIdentity
        //    var appIdentity = new ClaimsIdentity(claimsToAdd);
        //    ctx.Principal.AddIdentity(appIdentity);

        //    return Task.CompletedTask;
        //}
    }
}
