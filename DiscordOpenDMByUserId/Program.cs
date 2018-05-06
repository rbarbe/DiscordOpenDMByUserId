using DiscordApi;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace DiscordOpenDMByUserId
{
    class Program
    {
        [Option(ShortName = "t", LongName = "token", Description = "Discord access token")]
        public string Token { get; }

        [Option(ShortName = "i", LongName = "id", Description = "Discord user ID")]
        public ulong Id { get; }
        
        public static async Task Main(string[] args) => await CommandLineApplication.ExecuteAsync<Program>(args);

        private async Task<int> OnExecuteAsync(CommandLineApplication app)
        {
            if (string.IsNullOrEmpty(Token) || Id == 0)
            {
                app.ShowHelp();
                return 0;
            }
            
            var discordClient = new DiscordClient(Token);
            var currentUser = await discordClient.GetCurrentUserAsync();
            await discordClient.GetOrCreateDmChannelAsync(currentUser.Id, Id);

            return 1;
        }
    }
}
