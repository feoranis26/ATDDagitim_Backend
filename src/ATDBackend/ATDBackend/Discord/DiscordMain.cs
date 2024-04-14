using DSharpPlus;

namespace ATDBackend.Discord
{
    public static class DiscordMain
    {
        public static DiscordClient Client { get; private set; }

        public static async Task InitDiscord(ILogger logger)
        {
            Client = new DiscordClient(new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                TokenType = TokenType.Bot,
                Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN")
            });

            await Client.ConnectAsync();

            logger.LogInformation("Discord BOT Initialized");

            await Client.GetChannelAsync(1229091852834046064).Result.SendMessageAsync("Discord Bot Started");
        }
    }
}
