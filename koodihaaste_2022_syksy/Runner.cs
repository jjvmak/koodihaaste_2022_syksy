using koodihaaste_2022_syksy.Models;
using koodihaaste_2022_syksy.Services;
using koodihaaste_2022_syksy.SpectreOutput;

namespace koodihaaste_2022_syksy
{
    
    public class Runner
    {
        private IHeroService heroService;
        private StateMachine stateMachine;
        private IApiService apiService;

        public Runner(IHeroService heroService, IApiService apiService)
        {
            this.apiService = apiService;
            this.heroService = new HeroService(apiService);
            this.stateMachine = new StateMachine();
        }
        
        public async Task Run()
        {
             await StartScreen();
        }

        public async Task StartScreen()
        {
            Console.Clear();
            SpectreUtils.CreateHeader();
            var heroes = await SearchHero();
            if (heroes.Count == 0)
            {
                await NoResultsPage();
            } else
            {
                await ShowHeroTable(heroes);
            }
        }

        private async Task<List<HeroModel>> SearchHero()
        {
            SpectreUtils.WriteSimpleText("Etsi sankaria: ");
            
            var search = Console.ReadLine();
            var heroes = await heroService.SearchHeroes(search);
            
            await SpectreUtils.Proggres("prosessoidaan sankareita");
            
            SpectreUtils.WriteSimpleText($"Hakusanalla {search} löytyi {heroes.Count} sankaria");
            Console.Clear();
            
            return heroes;
        }

        private async Task ShowHeroTable(List<HeroModel> heroes)
        {
            SpectreUtils.ShowTableOfHeroes(heroes);
            SpectreUtils.WriteSimpleText("Valitse sankari(kirjoita rivin numero), tai suorita uusi haku (h)");
            var heroId = Console.ReadLine();
            if (heroId == "h")
            {
                await StartScreen();
            }
            else
            {
                SpectreUtils.WriteSimpleText("valittu sankari");
            }
        }

        private async Task NoResultsPage()
        {
            Console.Clear();
            SpectreUtils.WriteSimpleText("Ei hakutuloksia! Suorita uusi haku (paina mitä tahansa näppäintä)");
            Console.ReadLine();
            await StartScreen();
        }
    }
}
