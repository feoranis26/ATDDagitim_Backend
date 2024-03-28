using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security; //login and register operations
using ATDBackend.Utils; //Utilities
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization; //You know what this is...
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ATDBackend.Security
{
    public class CheckAuth() : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            var configuration = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IConfiguration>();
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDBContext>();
            var token = context
                .HttpContext
                .Request
                .Headers
                .Authorization
                .ToString()
                .Replace("Bearer ", "");
            token = token.Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JWTToken:SecurityKey"]);
            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JWTToken:Issuer"],
                        ValidAudience = configuration["JWTToken:Audience"],
                        ValidateLifetime = true
                    },
                    out SecurityToken validatedToken
                );
            }
            catch
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Unauthorized");
                return;
            }
            await next();
        }
    }
}
