using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ATDBackend.Database.DBContexts; //DB Contexts
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ATDBackend.Security
{
    public class CheckAuth(string? roleName = null) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            var configuration = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IConfiguration>(); //Configuration
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDBContext>(); //DB Context
            var token = context
                .HttpContext
                .Request
                .Headers
                .Authorization
                .ToString()
                .Replace("Bearer ", "");
            token = token.Trim(); //Get token
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

                var tokenUser = tokenHandler
                    .ReadJwtToken(token)
                    .Claims
                    .First(claim => claim.Type == "sub")
                    .Value;

                var user = dbContext.Users.Find(Convert.ToInt32(tokenUser));
                if (user == null)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    await context.HttpContext.Response.WriteAsync("Unauthorized_noUsr");
                    return;
                }
                if (roleName != null || roleName != "")
                {
                    var role = dbContext
                        .Roles
                        .FirstOrDefault(
                            x => x.Role_name.Equals(roleName, StringComparison.OrdinalIgnoreCase)
                        );
                    int role_id = -1;
                    if (role == null)
                    {
                        Console.WriteLine("Role not found");
                        throw new Exception("Role not found");
                    }
                    else
                    {
                        role_id = role.Id;
                        if (role_id != user.RoleId)
                        {
                            context.HttpContext.Response.StatusCode = 401;
                            await context.HttpContext.Response.WriteAsync("Unauthorized_role");
                            return;
                        }
                    }
                    if (role_id != user.RoleId)
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        await context.HttpContext.Response.WriteAsync("Unauthorized_role");
                        return;
                    }
                }
            }
            catch
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Unauthorized_tokenExp");
                return;
            }
            await next();
        }
    }
}
