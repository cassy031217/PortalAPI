using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using PortalAPI.Repository;
using System.ComponentModel.DataAnnotations;

namespace PortalAPI
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            // Gets the clientid and client secret from authenticate header
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                // try to get form values
                context.TryGetFormCredentials(out clientId, out clientSecret);

            }

            // Validate clientid and clientsecret. You can omit validating client secret if none is provided in your request (as in sample client request above)
            var validClient = !string.IsNullOrWhiteSpace(clientId);

            if (validClient)
            {
                // Need to make the client_id available for later security checks
                context.OwinContext.Set<string>("as:client_id", clientId);

                context.Validated();
            }
            else
            {
                context.Rejected();
            }

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {

            // Change authentication ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);

        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (context.ClientId == "Portal")
            {
                Accounts acc = new Accounts();
                if (acc.Login(context.UserName, context.Password))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    identity.AddClaim(new Claim("username", context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
            }
            else if (context.ClientId == "WinApp")
            {
                if (context.UserName == "Admin" && context.Password == "n@tcc0p0rt5l")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    identity.AddClaim(new Claim("username", context.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
            }
            else if (context.ClientId == "Admin")
            {
                //Admin condition here
            }

        }
    }

    public class RefreshTokenServerProvider : IAuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();

            // copy all properties and set the desired lifetime of refresh token
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            };

            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);

            _refreshTokens.TryAdd(guid, refreshTokenTicket);

            // consider storing only thr hash of the handle
            context.SetToken(guid);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw  new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            // context.DeserializeTicket(context.Token)
            AuthenticationTicket ticket;
            string header = context.OwinContext.Request.Headers["Authorization"];

            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
        }
    }
}