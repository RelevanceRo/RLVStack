﻿using Backend.Api;
using Backend.Api.Configuration;
using Backend.Application.Common;
using Backend.Common;
using Backend.Common.Interfaces;
using Backend.Features.Auth._Shared;
using Backend.Features.Roles._Shared;
using Backend.Features.Users._Shared;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Backend.Common.Models;
using Backend.Features.Auth.Token;

namespace Backend.Features.Auth;

public sealed class ExternalAuth
{

    public class GoogleAuthClient
    {
        public class GoogleTokens
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; } = string.Empty;

            [JsonPropertyName("id_token")]
            public string IdToken { get; set; } = string.Empty;
        }

        public class GoogleUserInfo
        {
            [JsonPropertyName("email")]
            public string Email { get; set; } = string.Empty;
            [JsonPropertyName("verified_email")]
            public bool VerifiedEmail { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;
            [JsonPropertyName("given_name")]
            public string GivenName { get; set; } = string.Empty;
            [JsonPropertyName("family_name")]
            public string FamilyName { get; set; } = string.Empty;
        }

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GoogleAuthClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<GoogleTokens> ExchangeCodeForTokensAsync(string code)
        {
            var clientId = _configuration["Authentication:Google:ClientId"];
            var clientSecret = _configuration["Authentication:Google:ClientSecret"];
            var redirectUri = _configuration["Authentication:Google:RedirectUri"];

            var tokenRequestParams = new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["code"] = code,
                ["grant_type"] = "authorization_code",
                ["redirect_uri"] = redirectUri
            };

            var response = await _httpClient.PostAsync(
                "https://oauth2.googleapis.com/token",
                new FormUrlEncodedContent(tokenRequestParams));

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<GoogleTokens>();
        }

        public async Task<GoogleUserInfo> GetUserInfoAsync(string accessToken)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
            requestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<GoogleUserInfo>();
        }
    }
    public sealed class GoogleAuthRequest
    {
        public string Code { get; set; } = string.Empty;
    }
    internal sealed class Handler(
    ITokenService tokenService,
    KrafterContext db,
    UserManager<KrafterUser> userManager,
    RoleManager<KrafterRole> roleManager,
    GoogleAuthClient googleAuthClient) : IScopedHandler
    {
        public async Task<Response<TokenResponse>> GetTokenAsync(GoogleAuthRequest request,
            CancellationToken cancellationToken)
        {
            // Get the auth info using the code
            var tokens = await googleAuthClient.ExchangeCodeForTokensAsync(
                request.Code);

            if (tokens == null)
            {
                throw new UnauthorizedException("Invalid token");
            }
            ;

            // Get user info from Google
            var userInfo = await googleAuthClient.GetUserInfoAsync(tokens.AccessToken);
            if (userInfo == null)
            {
                throw new UnauthorizedException("Invalid user info");
            }

            // Find or create user based on email
            var user = await userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
            {
                var basic = await roleManager.FindByNameAsync(KrafterRoleConstant.Basic);
                if (basic is null)
                {
                    throw new NotFoundException("Basic Role Not Found.");
                }

                user = new KrafterUser
                {
                    IsActive = true,

                    FirstName = userInfo.GivenName,
                    LastName = userInfo.Email,
                    Email = userInfo.Email,
                    EmailConfirmed = userInfo.VerifiedEmail,
                    PhoneNumber = userInfo.Email,
                    UserName = userInfo.Email,
                    Id = Guid.NewGuid().ToString()
                };
                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    user.UserName = user.Email;
                }
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new KrafterException("An error occurred while creating user.");
                }

                db.UserRoles.Add(
                    new KrafterUserRole()
                    {
                        RoleId = basic.Id,
                        UserId = user.Id,
                    });
                await db.SaveChangesAsync(new List<string>(), true, cancellationToken: cancellationToken);
            }

            var res = await tokenService.GenerateTokensAndUpdateUser(user.Id, string.Empty);
            return new Response<TokenResponse>()
            {
                Data = res
            };
        }
    }

    public sealed class GoogleAuthRoute : IRouteRegistrar
    {
        public void MapRoute(IEndpointRouteBuilder app)
        {
            var productGroup = app.MapGroup(KrafterRoute.ExternalAuth)
                .AddFluentValidationFilter();
            // Token exchange endpoint
            productGroup.MapPost("/google", async (
                HttpContext context,
                GoogleAuthRequest request,
                Handler externalAuthService,
                KrafterContext db,
                UserManager<KrafterUser> userManager, RoleManager<KrafterRole> roleManager) =>
            {
                var ipAddress = GetIpAddress(context);
                var res = await externalAuthService.GetTokenAsync(request, CancellationToken.None);
                return Results.Json(res, statusCode: res.StatusCode);
            }).AllowAnonymous();
        }
        private string? GetIpAddress(HttpContext httpContext)
        {
            return httpContext.Request.Headers.ContainsKey("X-Forwarded-For")
                ? httpContext.Request.Headers["X-Forwarded-For"]
                : httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
        }
    }
}
