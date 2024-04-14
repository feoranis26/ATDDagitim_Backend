using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Discord.Extensions;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ATDBackend.Discord.Commands
{
    public class Commands_Misc(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext appDbContext) : ApplicationCommandModule
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext dbContext = appDbContext;


        [SlashCommand("ping", "Replies with pong")]
        public async Task Ping(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync("Pong");
            await ctx.EditResponseAsync(string.Join(" , ", dbContext.Seeds.ToList().Select(x => x.Name)));
        }

    }
}
