using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Utils;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ATDBackend.Discord.AutoCompletes
{
    public class AutoComplete_Category(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext appDbContext) : IAutocompleteProvider
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext dbContext = appDbContext;

        public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
        {
            try
            {
                Console.WriteLine("AYTOCOMPELTE");
                string? categoryStr = ctx.OptionValue?.ToString()?.ToLower();
                Console.WriteLine(categoryStr);
                Dictionary<int, string> categories = new Dictionary<int, string>();

                List<DiscordAutoCompleteChoice> choices = new();

                if (categoryStr.IsNullOrEmpty()) (await dbContext.Categories.ToListAsync()).ForEach(x => choices.Add(new(x.CategoryName, x.Id)));
                else (await dbContext.Categories.Where(x => x.CategoryName.ToLower().Contains(categoryStr)).ToListAsync()).ForEach(x => choices.Add(new(x.CategoryName, x.Id)));

                Console.WriteLine($"LEN: {choices.Count} // {string.Join(" / ", choices.Select(x => x.Name))}");
                return choices;
            }
            catch(Exception ex)
            {
                Console.WriteLine("AAA \n" + JsonConvert.SerializeObject(ex));
                return new List<DiscordAutoCompleteChoice>();
            }
        }
    }
}
