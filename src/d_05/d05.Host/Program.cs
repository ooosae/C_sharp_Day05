using Microsoft.Extensions.Configuration;
using d05.Nasa.Apod;

namespace d05.Host;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: dotnet run <api_key> <result_count>");
            return;
        }

        if (args[0].ToLower() != "apod")
        {
            Console.WriteLine("The first argument must be 'apod'.");
            return;
        }

        if (!int.TryParse(args[1], out int resultCount) || resultCount <= 0)
        {
            Console.WriteLine("The second argument must be a positive integer.");
            return;
        }

        string filePath = Path.Combine(AppContext.BaseDirectory, "../../../appsettings.json");

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(filePath, optional: false, reloadOnChange: true)
            .Build();

        var appSettings = new AppSettings();
        configuration.Bind(appSettings);
        
        if (appSettings.ApiKey == null)
        {
            Console.WriteLine("No API key!");
            return;
        }

        var apodClient = new ApodClient(appSettings.ApiKey);

        var mediaList = await apodClient.GetAsync(resultCount);

        foreach (var media in mediaList)
        {
            Console.WriteLine($"{media.Date}\n'{media.Title}' by {media.Copyright}\n{media.Explanation}\n{media.Url}\n");
        }
    }
}