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
            SpectreUtils.CreateHeader("TAISTELEVAT PORKKANAT");

            /*
             * Jos peliä ei voida luoda vielä: haetaan hero1 ja hero 2
             * ja asetetaan ne.
             * 
             * Jos peli on luotu siirrytään taistelunäkymään ja käynnistetään gameloop.
             * 
             * **/

            if(!stateMachine.CanCreateGame())
            {
                var heroes = await SearchHero();
                var searchState = stateMachine.SetSearchResult(heroes);
                if (searchState == ActionResult.Succes) await SelectHeroTable(heroes);
                if (searchState == ActionResult.Fail) await NoResultsPage();
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

        private async Task SelectHeroTable(List<HeroModel> heroes)
        {
            SpectreUtils.ShowTableOfHeroes(heroes);
            SpectreUtils.WriteSimpleText("Valitse sankari (id), tai suorita uusi haku (h)");
            var heroId = Console.ReadLine();

            if (heroId == "h")
            {
                await StartScreen();
            }
            else
            {
                var succes = int.TryParse(heroId, out int result);
                if (succes) await StartScreen(); // go to selection confirmation screen
                if (!succes) await ErrorScreen("Virheellinen sankari id!");
            }
        }

        private async Task NoResultsPage()
        {
            Console.Clear();
            SpectreUtils.WriteSimpleText("Ei hakutuloksia! Suorita uusi haku (enter)");
            Console.ReadLine();
            await StartScreen();
        }

        private async Task ErrorScreen(string message) 
        {
            Console.Clear();
            SpectreUtils.CreateHeader("ERROR");
            SpectreUtils.WriteSimpleText(message);
            SpectreUtils.WriteSimpleText("Paina enter jatkaaksesi");
            Console.ReadLine();
            await StartScreen();
        }
    }
}
