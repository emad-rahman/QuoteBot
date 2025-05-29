using Discord.Interactions;

namespace QuoteBot
{
    public class CommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("quote", "Get a random quote")]
        public async Task Quote()
        {
            var quote = await QuoteGenerator.GetRandomQuote();

            if (quote?.Content == null || quote.Author == null)
            {
                await RespondAsync("\"Something went wrong\" - Emad Rahman");
                return;
            }

            var quoteText = $"\"{quote.Content}\" - {quote.Author}";
            await RespondAsync(quoteText);
        }
    }
}
