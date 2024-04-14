using ATDBackend.Discord.Extensions;
using DSharpPlus.SlashCommands;

namespace ATDBackend.Discord.Commands
{
    public class Commands_Misc : ApplicationCommandModule
    {
        [SlashCommand("ping", "Replies with pong!")]
        public async Task Ping(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync("PONGGGGG");
        }
    }
}
