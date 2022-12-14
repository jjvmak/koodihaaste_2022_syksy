using koodihaaste_2022_syksy;
using koodihaaste_2022_syksy.Services;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvide = new ServiceCollection()
            .AddSingleton<IHeroService, HeroService>()
            .AddSingleton<IApiService, ApiService>()
            .BuildServiceProvider();

        var runner = new Runner(
                serviceProvide.GetService<IHeroService>(),
                serviceProvide.GetService<IApiService>()
        );

        StartSequence(runner);
    }
    
    public static async Task StartSequence(Runner runner) {
        // To prevent console app from shutting down
        // wait for the task to complete
        runner.Run().Wait();
    }
}