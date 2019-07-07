namespace Dnd.CliApp
{
    using System;
    using System.CommandLine;
    using System.CommandLine.DragonFruit;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Dnd.FeedManagement.Core;
    using Dnd.Meta.Core;
    using Dnd.Meta.Core.Abstractions;
    using Dnd.WebHost.Core;

    internal class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var startCommand = new Command("start")
            {
                new  Option("port")
                {
                    Argument = new Argument<int>
                    {
                        Arity = ArgumentArity.ZeroOrOne
                    }
                }
            };
            var method = typeof(Program).GetMethod(nameof(StartAsync));
            startCommand.ConfigureFromMethod(method, () => null);
            var setupCommand = new Command("setup")
            {
                new Option("organization")
                {
                    Argument = new Argument<string>
                    {
                        Arity = ArgumentArity.ExactlyOne
                    }
                },
                new Option("username")
                {
                    Argument = new Argument<string>
                    {
                        Arity = ArgumentArity.ExactlyOne
                    }
                },
                new Option("token")
                {
                    Argument = new Argument<string>
                    {
                        Arity = ArgumentArity.ExactlyOne
                    }
                }
            };
            method = typeof(Program).GetMethod(nameof(SetupAsync));
            setupCommand.ConfigureFromMethod(method, () => null);
            var rootCommand = new RootCommand
            {
                startCommand,
                setupCommand
            };
            return await rootCommand.InvokeAsync(args);
        }

        public static async Task SetupAsync(string organization, string username, string token)
        {
            var services = new ServiceCollection().AddLogging().AddMetaServices().BuildServiceProvider();
            await services.GetRequiredService<IMetaService>().SaveAzureCredentialsAsync(organization, username, token);
            services.GetRequiredService<ILoggerFactory>().CreateLogger("Cli").LogInformation("Settings saved");
        }

        public static async Task StartAsync(int port = 8089)
        {
            var host = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder<Startup>(Array.Empty<string>()).ConfigureServices(
                    services => services.AddAutoMapper(typeof(Startup).Assembly).AddMetaServices()
                    .AddPackagesServices()
                        .AddWebServices())
                .UseUrls($"http://+:{port}").Build();
            host.Services.GetRequiredService<IMapper>().ConfigurationProvider.CompileMappings();
            await host.RunAsync();
        }
    }
}