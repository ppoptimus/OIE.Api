using ApplicationCore;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace OIE.Api
{
    public class Config
    {
        public const string Authenticated = "Authenticated";
        public const string SelfAssessmentPolicy = "SelfAssessment";
        public const string PMIPolicy = "PMI";
        public const string SurveyPolicy = "Survey";
        public const string AdminPolicy = "Admin";

        // Identity resources (used by UserInfo endpoint).
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResources.Email(),
                new IdentityResource("roles", new List<string> { JwtClaimTypes.Role }) ,
                //new IdentityResource
                //{
                //    Name = "MyApiIdentityScope",
                //    UserClaims = {
                //        JwtClaimTypes.Email,
                //        JwtClaimTypes.EmailVerified,
                //        JwtClaimTypes.PhoneNumber,
                //        JwtClaimTypes.PhoneNumberVerified,
                //        JwtClaimTypes.GivenName,
                //        JwtClaimTypes.FamilyName,
                //        JwtClaimTypes.PreferredUserName
                //    }
                //}
            };
        }

        // Api resources.
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //new ApiResource("OieApi", "My API")
                new ApiResource("OieApi" ) {
                    UserClaims = {
                        //JwtClaimTypes.GivenName,
                        //JwtClaimTypes.FamilyName,
                        //JwtClaimTypes.PreferredUserName,
                        JwtClaimTypes.Role
                    }
                }
            };
        }

        // Clients want to access resources.
        public static IEnumerable<Client> GetClients()
        {
            // Clients credentials.
            return new List<Client>
            {
                // http://docs.identityserver.io/en/release/reference/client.html.
                new Client
                {
                    ClientId = "OIE-WEB",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Resource Owner Password Credential grant.
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false, // This client does not need a secret to request tokens from the token endpoint.

                    //AccessTokenLifetime = 120, // Lifetime of access token in seconds.

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "OieApi"
                    },
                    AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    //AbsoluteRefreshTokenLifetime = 60,
                    //SlidingRefreshTokenLifetime = 60,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowedCorsOrigins = new List<string>
                    {
                        ServerUtils.ConfigInfo.WebServer,
                        "http://localhost:4200"//TODO:Remove
                    }
                },
                new Client
                {
                    ClientId = "SWAGGER",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Resource Owner Password Credential grant.
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false, // This client does not need a secret to request tokens from the token endpoint.

                    AccessTokenLifetime = 9000, // Lifetime of access token in seconds.

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "OieApi"
                    },
                    AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AbsoluteRefreshTokenLifetime = 7200,
                    SlidingRefreshTokenLifetime = 9000,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowedCorsOrigins = new List<string>
                    {
                        ServerUtils.ConfigInfo.APIServer
                    }
                }
            };
        }
    }
}
