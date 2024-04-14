using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Discord.Extensions;
using DSharpPlus.SlashCommands;

namespace ATDBackend.Discord.Commands
{
    [SlashCommandGroup("seed", "commands related to seed operations,")]
    public class Commands_Seed(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context) : ApplicationCommandModule
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;


        [SlashCommand("add", "Add a seed to the database")]
        public async Task AddSeed(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync(_context == null ? "APP DB CONTEXT IS NULLLLLLLLLLLLLLLLLLLL" : "APP DB CONTEXT IS NOT NULLLLLLLLLLLLLLLLLLLL");
        }
    }
}
