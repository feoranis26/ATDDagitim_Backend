using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ATDBackend.Security.SessionSystem
{
    [Flags] //!!!!!!! LAST SHIFT: 13
    public enum Permission : ulong
    {
        None = 0,

        PERMISSION_ADMIN = 1 << 0,  // 1

        ORDER_SELF_READ = 1 << 1,//2
        ORDER_SELF_CREATE = 1 << 2,//4
        ORDER_GLOBAL_READ = 1 << 3,//8
        ORDER_GLOBAL_MODIFY = 1 << 4,//16


        PRODUCT_CREATE = 1 << 5,//32
        PRODUCT_MODIFY = 1 << 6,//64
        PRODUCT_CONTRIBUTOR_MODIFY = 1 << 13,//8192

        SCHOOL_SELF_READ = 1 << 7,//128
        SCHOOL_GLOBAL_CREATE = 1 << 8,//256
        SCHOOL_GLOBAL_READ = 1 << 9,//512
        SCHOOL_GLOBAL_MODIFY = 1 << 10,//1024

        USER_SELF_READ = 1 << 11,//2048
        USER_SELF_BASKET = 1 << 12,//4096


    }


    public class RequireAuth(Permission requiredPermissions) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IConfiguration>(); //Configuration

            var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDBContext>(); //DB Context

            string? sessionid = context.HttpContext.Request.Cookies["sid"];

            if (sessionid == null)
            {
                context.Result = UnauthorizedResponse("nosession");
                return;
            };

            Session? session = SessionHandler.GetSessionBySID(sessionid);

            if(session == null)
            {
                context.Result = UnauthorizedResponse("nosession");
                return;
            };

            if(session.IsExpired)
            {
                SessionHandler.RemoveSession(session);
                context.Result = UnauthorizedResponse("expired");
                return;
            }


            User? user = dbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == session.UserID);
            
            if(user == null)
            {
                context.Result = UnauthorizedResponse("unauthorized");
                return;
            }

            ulong userperms = user.Role.Permissions;

            if (UserHasPerm(userperms, (ulong)requiredPermissions))
            {
                SessionHandler.ExtendSessionExpire(session);
                await next();
            }
            else
            {
                context.Result = UnauthorizedResponse("unauthorized");
                return;
            }
        }


        private static bool UserHasPerm(ulong userPermissions, ulong requiredPermissions)
        {
            bool userHasRequiredPerms = (userPermissions & (ulong)requiredPermissions) == (ulong)requiredPermissions;
            bool noPermissionRequired = requiredPermissions == (ulong)Permission.None;
            bool isUserAdmin = (userPermissions & (ulong)Permission.PERMISSION_ADMIN) == (ulong)Permission.PERMISSION_ADMIN;

            return userHasRequiredPerms || noPermissionRequired || isUserAdmin;
        }

        private static IActionResult UnauthorizedResponse(object? value = null)
        {
            return new ObjectResult(value)
            {
                StatusCode = 401
            };
        }
    }
}
