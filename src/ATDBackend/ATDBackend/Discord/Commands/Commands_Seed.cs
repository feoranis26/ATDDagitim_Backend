using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Discord.AutoCompletes;
using ATDBackend.Discord.Extensions;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Discord.Commands
{
    
    public class Commands_Seed(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext appDbContext) : ApplicationCommandModule
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext dbContext = appDbContext;

        [SlashCommand("add", "Creates a new seed")]
        public async Task Ping(InteractionContext ctx, [Autocomplete(typeof(AutoComplete_Category))][Option("cateogry", "Seed category", true)] int id)
        {
            try
            {
                await ctx.DeferAsync();

                await ctx.EditResponseAsync(id.ToString());
            }
            catch(Exception ex)
            {
                await ctx.EditResponseAsync(ex.Message);
            }
        }
    }
}
