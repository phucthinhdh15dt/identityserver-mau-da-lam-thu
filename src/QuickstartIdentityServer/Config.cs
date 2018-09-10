// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuickstartIdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                
                new IdentityResource {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
            }




            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
    {
        new ApiResource("api")
        {
            Name = "api",
 
            // secret for introspection endpoint
            ApiSecrets =
            {
                new Secret("secret".Sha256())
            },

            

            // claims to include in access token
            UserClaims =
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.Email,
                "role"
            },
 
            // API has multiple scopes
            Scopes =
            {
                new Scope
                {
                    Name = "api.read",
                    DisplayName = "Full access read",
                },
                new Scope
                {
                    Name = "api.write",
                    DisplayName = "Full access write",
                    Emphasize = true,
 
                }
            }
        }
    };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "calendar.read_only", "calendar.full_access" }
                },


                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    ClientClaimsPrefix= "status",


                    RedirectUris = { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                       

                         "api.read","api.write"
                    },
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "adminrole"),
                        new Claim("website", "https://alice.com"),
                        

                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    

                    Claims = new List<Claim>
                    {
                        new Claim("name", "userrole"),
                        new Claim("website", "https://bob.com"),


                    }
                }
            };
        }
    }
}