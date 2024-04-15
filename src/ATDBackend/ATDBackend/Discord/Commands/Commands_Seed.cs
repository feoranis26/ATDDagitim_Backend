using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Discord.AutoCompletes;
using ATDBackend.Discord.Extensions;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Discord.Commands
{
    [SlashCommandGroup("seed", "Seed commands")]
    public class Commands_Seed(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext appDbContext) : ApplicationCommandModule
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext dbContext = appDbContext;

        [SlashCommand("add", "Creates a new seed")]
        public async Task Ping(InteractionContext ctx,
            [Option("name", "Name of the seed")] string name,
            [Option("description", "Description of the seed")] string description,
            [Autocomplete(typeof(AutoComplete_Category))][Option("cateogry", "Seed category", true)] string categoryid_str,
            [Option("image", "Image of the seed")] DiscordAttachment image_att,
            [Option("price", "Price of the seed")] string price_str,
            [Option("is_active", "Whether if the seed is active or not")] bool isActive,
            [Autocomplete(typeof(AutoComplete_School))][Option("contributor_school", "Contributor school of the seed")] string cschoolid_str,
            [Option("stock", "Stock of the seed")] string stock_str
            )
        {
            try
            {
                await ctx.DeferAsync();

                if (!int.TryParse(price_str, out int price))
                {
                    await ctx.EditResponseAsync("Invalid price");
                    return;
                }

                if (!int.TryParse(stock_str, out int stock))
                {
                    await ctx.EditResponseAsync("Invalid stock");
                    return;
                }

                byte[]? image = await image_att.GetFileContentAsync();

                if (image == null)
                {
                    await ctx.EditResponseAsync("Couldn't fetch image");
                    return;
                }

                if (!int.TryParse(categoryid_str, out int categoryId) || dbContext.Categories.Find(categoryId) == null)
                {
                    await ctx.EditResponseAsync("Invalid category id");
                    return;
                }

                if (!int.TryParse(cschoolid_str, out int cschoolId) || dbContext.Schools.Find(cschoolId) == null)
                {
                    await ctx.EditResponseAsync("Invalid school id");
                    return;
                }




                Seed seed = new Seed()
                {
                    Name = name,
                    Description = description,
                    CategoryId = categoryId,
                    Image = image,
                    Price = price,
                    Is_active = isActive,
                    Stock = stock,
                    Date_added = DateTime.Now.ToUniversalTime()
                };

                dbContext.Seeds.Add(seed);
                await dbContext.SaveChangesAsync();

                seed.SeedContributors = new List<SeedContributor>()
                {
                    new SeedContributor()
                    {
                        SeedId = seed.Id,
                        SchoolId = cschoolId
                    }
                };
                await dbContext.SaveChangesAsync();

                await ctx.EditResponseAsync("Success");
            }
            catch(Exception ex)
            {
                await ctx.EditResponseAsync("An error occured / " + ex.Message);
                _logger.LogError(ex, "Error");
            }
        }
    }
}
