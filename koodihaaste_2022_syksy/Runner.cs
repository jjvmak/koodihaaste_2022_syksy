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

        private HeroModel? hero1;
        private HeroModel? hero2;

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
            SpectreUtils.CreateCenterBlinking("-- PAINA ENTER --");
            var search = Console.ReadLine();

            await MainMenu();
            
        }

        private async Task MainMenu()
        {
            Console.Clear();
            SpectreUtils.CreateHeader("TAISTELEVAT PORKKANAT");

            SpectreUtils.SelectedHeroes(hero1, hero2);
            
            /*
              * Jos peliä ei voida luoda vielä: haetaan hero1 ja hero 2
              * ja asetetaan ne.
              * 
              * Jos peli on luotu siirrytään taistelunäkymään ja käynnistetään gameloop.
              * 
              * **/

            if (!stateMachine.CanCreateGame())
            {
                var heroes = await SearchHero();
                var searchState = stateMachine.SetSearchResult(heroes);
                if (searchState == ActionResult.Succes) await SelectHeroTable(heroes);
                if (searchState == ActionResult.Fail) await NoResultsPage();
            }
            else
            {
                hero1.Color = "red";
                hero2.Color = "blue";
                var game = new Game(hero1, hero2);
                var gameLoop = new GameLoop(game);
                gameLoop.StartGame();

                // reset game
                stateMachine = new StateMachine();
                hero1 = null;
                hero2 = null;
                await StartScreen();
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
                await MainMenu();
            }
            else
            {
                var succes = int.TryParse(heroId, out int result);
                if (succes) 
                {
                    if (!stateMachine.IsHero1Selected())
                    {
                        hero1 = heroes[result];
                        stateMachine.SetHeroOne();
                    }
                    else if (!stateMachine.IsHero2Selected()) { 
                        hero2 = heroes[result];
                        stateMachine.SetHeroTwo();
                    }
                } // go to selection confirmation screen
                if (!succes) await ErrorScreen("Virheellinen sankari id!");
                await MainMenu();
            }
        }

        private async Task NoResultsPage()
        {
            Console.Clear();
            SpectreUtils.WriteSimpleText("Ei hakutuloksia! Suorita uusi haku (enter)");
            Console.ReadLine();
            await MainMenu();
        }

        private async Task ErrorScreen(string message) 
        {
            Console.Clear();
            SpectreUtils.CreateHeader("ERROR");
            SpectreUtils.WriteSimpleText(message);
            SpectreUtils.WriteSimpleText("Paina enter jatkaaksesi");
            Console.ReadLine();
            await MainMenu();
        }
    }
}
