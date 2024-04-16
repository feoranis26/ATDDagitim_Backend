using ATDBackend.Controllers;
using ATDBackend.Database.DBContexts;
using ATDBackend.Utils;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;

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
            string? input = ctx.OptionValue?.ToString()?.ToLower();

            List<DiscordAutoCompleteChoice> choices = new();

            if (input.IsNullOrEmpty()) (await dbContext.Categories.ToListAsync()).ForEach(x => choices.Add(new(x.CategoryName, x.Id.ToString())));
            else (await dbContext.Categories.Where(x => x.CategoryName.ToLower().Contains(input)).ToListAsync()).ForEach(x => choices.Add(new(x.CategoryName, x.Id.ToString())));

            return choices;
        }
    }
}
