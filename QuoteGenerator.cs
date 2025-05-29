using QuoteBot.Models;
using System.Text.Json;

namespace QuoteBot
{
    public class QuoteGenerator
    {
        public static readonly string Url = "https://api.quotable.io/random";

        public static async Task<Quote?> GetRandomQuote()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var httpClient = new HttpClient(handler);

            string response = await httpClient.GetStringAsync(Url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<Quote>(response, options);
        }
    }
}
