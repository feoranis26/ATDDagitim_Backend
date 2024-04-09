using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ATDBackend.Database.DBContexts; //DB Contexts
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ATDBackend.Security
{
    [Flags]
    public enum Permission : ulong
    {
        None = 0,

        PERMISSION_ADMIN =  1 << 0,  // 1

        ORDER_SELF_READ       = 1 << 1,
        ORDER_SELF_CREATE     = 1 << 2,
        ORDER_GLOBAL_READ     = 1 << 3,
        ORDER_GLOBAL_MODIFY   = 1 << 4,

        PRODUCT_CREATE        = 1 << 5,
        PRODUCT_MODIFY        = 1 << 6,

        SCHOOL_SELF_READ      = 1 << 7,
        SCHOOL_GLOBAL_CREATE  = 1 << 8,
        SCHOOL_GLOBAL_READ    = 1 << 9,
        SCHOOL_GLOBAL_MODIFY  = 1 << 10,

        USER_SELF_READ        = 1 << 11,
        USER_SELF_BASKET      = 1 << 12,

    }


    public class RequireAuth(Permission requiredPermissions) : ActionFilterAttribute
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
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECURITYKEY"));

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
                await context.HttpContext.Response.WriteAsync("Unauthorized_token");
                return;
            }

            var tokenUser = tokenHandler
                .ReadJwtToken(token)
                .Claims
                .First(claim => claim.Type == "sub")
                .Value;
            Console.WriteLine("Token Claims: ", tokenHandler.ReadJwtToken(token).Claims);
            Console.WriteLine("TOKEN USER: ", tokenUser.ToString());

            int userID = Convert.ToInt32(tokenUser);

            var user = dbContext.Users.Include(U => U.Role).FirstOrDefault(U => U.Id == userID);

            
            if (user != null && user.Role != null)
            {
                Console.WriteLine("USERID: ", user);
                context.HttpContext.Items["User"] = user;
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Unauthorized_noUsr");
                return;
            }

            ulong userPermissions = user.Role.Permissions;

            bool userHasRequiredPerms = (userPermissions & (ulong)requiredPermissions) == (ulong)requiredPermissions;
            bool noPermissionRequired = requiredPermissions == Permission.None;
            bool isUserAdmin = (userPermissions & (ulong)Permission.PERMISSION_ADMIN) == (ulong)Permission.PERMISSION_ADMIN;

            if (userHasRequiredPerms || noPermissionRequired || isUserAdmin)
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("Unauthorized_role");
                return;
            }
        }
    }
}
