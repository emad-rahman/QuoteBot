using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace QuoteBot
{
    public class Program
    {
        private static DiscordSocketClient? _client;

        public static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            var token = configuration.GetValue<string>("DiscordToken");

            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += OnClientReady;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private static async Task OnClientReady()
        {
            Console.WriteLine("Bot is ready!");

            var interactionService = new InteractionService(_client);

            // Add modules from the current assembly
            await interactionService.AddModulesAsync(typeof(Program).Assembly, null);
            await interactionService.RegisterCommandsGloballyAsync();

            _client.InteractionCreated += async interaction =>
            {
                var ctx = new SocketInteractionContext(_client, interaction);
                await interactionService.ExecuteCommandAsync(ctx, null);
            };
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}