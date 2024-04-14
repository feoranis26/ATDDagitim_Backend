using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Discord.Extensions;
using DSharpPlus.SlashCommands;

namespace ATDBackend.Discord.Commands
{
    [SlashCommandGroup("seed", "commands related to seed operations,")]
    public class Commands_Seed() : ApplicationCommandModule
    {


        [SlashCommand("add", "Add a seed to the database")]
        public async Task AddSeed(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync("AA");
            //await ctx.EditResponseAsync(_context == null ? "APP DB CONTEXT IS NULLLLLLLLLLLLLLLLLLLL" : "APP DB CONTEXT IS NOT NULLLLLLLLLLLLLLLLLLLL");
        }

        [SlashCommand("a", "bb")]
        public async Task a(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync("AAb");
            //await ctx.EditResponseAsync(_context == null ? "APP DB CONTEXT IS NULLLLLLLLLLLLLLLLLLLL" : "APP DB CONTEXT IS NOT NULLLLLLLLLLLLLLLLLLLL");
        }

        [SlashCommand("ping", "Replies with pong!")]
        public async Task Ping(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync("PONGGGGG");
        }
    }
}
