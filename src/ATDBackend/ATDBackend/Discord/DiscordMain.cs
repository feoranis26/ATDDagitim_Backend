using ATDBackend.Discord.Commands;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;

namespace ATDBackend.Discord
{
    public static class DiscordMain
    {
        public static DiscordClient Client { get; private set; }

        public static async Task InitDiscord(IServiceProvider services)
        {



            Client = new DiscordClient(new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                TokenType = TokenType.Bot,
                Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN"),
                MinimumLogLevel = LogLevel.Debug
            });

            var slash = Client.UseSlashCommands(new SlashCommandsConfiguration()
            {
                Services = services
            });

            slash.RegisterCommands<Commands_Misc>();
            slash.RegisterCommands<Commands_Seed>();

            await Client.ConnectAsync();



            await Client.GetChannelAsync(1229091852834046064).Result.SendMessageAsync("Discord Bot Started");


            slash.AutocompleteErrored += Slash_AutocompleteErrored;
        }

        private static Task Slash_AutocompleteErrored(SlashCommandsExtension sender, DSharpPlus.SlashCommands.EventArgs.AutocompleteErrorEventArgs args)
        {
            Console.WriteLine("AUTOCOMPLETE ERROR");
            Console.WriteLine(JsonConvert.SerializeObject(args.Exception));
            return Task.CompletedTask;
        }
    }
}
