using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace RenStore.Identity.DuendeServer.WebAPI.Data.IdentityConfigurations;

public static class Configuration
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AuthConstants.AUTH_IDENTITY_CLIENT_ID,
                         AuthConstants.AUTH_IDENTITY_DISPLAY_NAME)
        };
        
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource(AuthConstants.AUTH_IDENTITY_CLIENT_ID,
                            AuthConstants.AUTH_IDENTITY_DISPLAY_NAME, 
                            new [] { JwtClaimTypes.Name })
            {
                Scopes = { AuthConstants.AUTH_IDENTITY_CLIENT_ID }
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = AuthConstants.AUTH_IDENTITY_CLIENT_ID,
                ClientName = AuthConstants.AUTH_IDENTITY_CLIENT_NAME,
                
                AllowedGrantTypes = GrantTypes.Code,
                AccessTokenType = AccessTokenType.Jwt,
                
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://.../signin-oidc",
                    "https://localhost:7226/Home/Index/",

                },
                 
                
                
                AllowedCorsOrigins =
                {
                    "http://..."
                },
                
                PostLogoutRedirectUris =
                {
                    "http://.../signout-oidc",
                },
                
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    AuthConstants.AUTH_IDENTITY_CLIENT_ID
                },
                
                AllowAccessTokensViaBrowser = true
            }
        };
}