// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[] 
            { 
                //"api1.Read/Write"
                new ApiResource ("api1","My API 1") 
            };
        
        public static IEnumerable<Client> Clients =>
           new List<Client>
            {
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "My MVC Client App",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequireConsent = true,
                    //RequirePkce = true,
                
                    // where to redirect to after login
                    RedirectUris = { "http://localhost:8097/signin-oidc" },
                    
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:8097/signout-callback-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1"
                    },
                    AllowOfflineAccess = true
                },

                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientName = "My API",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    // scopes that client has access to
                    AllowedScopes = { "api1" },
                    AllowOfflineAccess= true
                }
            };

    }
}