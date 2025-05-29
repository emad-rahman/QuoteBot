using Discord.Interactions;

namespace QuoteBot
{
    public class CommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("quote", "Get a random quote")]
        public async Task Quote()
        {
            var quote = await QuoteGenerator.GetRandomQuote();
            await RespondAsync(quote);
        }
    }
}
