using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace ATDBackend.Discord.AutoCompletes
{
    public class AutoComplete_Category : IAutocompleteProvider
    {
        public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
        {
            string item = ctx.OptionValue.ToString();

            return null;
        }
    }
}
