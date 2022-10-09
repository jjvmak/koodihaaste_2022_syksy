using koodihaaste_2022_syksy.Services;
using koodihaaste_2022_syksy.SpectreOutput;

namespace koodihaaste_2022_syksy
{
    
    public class Runner
    {
        private IHeroService heroService;
        private IApiService apiService;

        public Runner(IHeroService heroService, IApiService apiService)
        {
            this.apiService = apiService;
            this.heroService = new HeroService(apiService);
        }
        
        public async Task Run()
        {
            SpectreUtils.CreateHeader();
            await SearchHero();
        }

        private async Task SearchHero()
        {
            SpectreUtils.WriteSimpleText("Etsi sankaria: ");
            var search = Console.ReadLine();
            var heroes = await heroService.SearchHeroes(search);
            await SpectreUtils.Proggres("prosessoidaan sankareita");
            SpectreUtils.WriteSimpleText($"Hakusanalla {search} löytyi {heroes.Count} sankaria");
        }
    }
}
