using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Discord.Extensions;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Discord.Commands
{
    //[SlashCommandGroup("seed", "commands related to seed operations,")]
    public class Commands_Seed(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        IDbContextFactory<AppDBContext> dbFactory) : ApplicationCommandModule
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly IDbContextFactory<AppDBContext> dbFactory = dbFactory;


        [SlashCommand("add", "Add a seed to the database")]
        public async Task AddSeed(InteractionContext ctx)
        {
            using AppDBContext db = dbFactory.CreateDbContext();

            try
            {
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                await ctx.DeferAsync();

                await ctx.EditResponseAsync("AA");
                await ctx.EditResponseAsync(string.Join(" , ", db.Seeds.ToList().Select(x => x.Name)));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"THE ERRR / {ex.Message} / {ex.StackTrace}");
            }
        }

        [SlashCommand("a", "bb")]
        public async Task a(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync("AAb");
            //await ctx.EditResponseAsync(_context == null ? "APP DB CONTEXT IS NULLLLLLLLLLLLLLLLLLLL" : "APP DB CONTEXT IS NOT NULLLLLLLLLLLLLLLLLLLL");
        }
    }
}
