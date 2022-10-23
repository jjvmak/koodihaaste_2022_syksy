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
            Console.ReadLine();

            await MainMenu();
        }

        private async Task MainMenu()
        {
            Console.Clear();
            SpectreUtils.CreateHeader("TAISTELEVAT PORKKANAT");

            SpectreUtils.SelectedHeroes(hero1, hero2);

            // Check if the game can be created
            // If not: proceed to hero search
            if (!stateMachine.CanCreateGame())
            {
                var heroes = await SearchHero();
                var searchState = stateMachine.SetSearchResult(heroes);
                if (searchState == ActionResult.Succes) await SelectHeroTable(heroes);
                if (searchState == ActionResult.Fail) await NoResultsPage();
            }
            // Game can be created
            else
            {
                // Set colors for contenters
                hero1.Color = "red";
                hero2.Color = "blue";
                // Create game, gameloop and start the gameloop
                var game = new Game(hero1, hero2);
                var gameLoop = new GameLoop(game);
                gameLoop.StartGame();

                // After the game is over, reset the state machine, heroes and go back to start screen
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
            var heroes = await heroService.SearchHeroes(search ?? "");
            
            await SpectreUtils.Proggres("prosessoidaan sankareita");
            
            Console.Clear();
            
            return heroes;
        }

        private async Task SelectHeroTable(List<HeroModel> heroes)
        {
            SpectreUtils.ShowTableOfHeroes(heroes);
            SpectreUtils.WriteSimpleText("Valitse sankari (id), tai suorita uusi haku (h)");
            SpectreUtils.DisplaySpecialityBonuses();
            var heroId = Console.ReadLine();

            if (heroId == "h")
            {
                await MainMenu();
            }
            else
            {
                var succes = int.TryParse(heroId, out int idIndex);
                
                if (succes) 
                {

                    // Check if the ID can be selected
                    // ID cant be negative or larger than the list
                    if ( idIndex < 0 || idIndex > heroes.Count - 1) await ErrorScreen("Virheellinen sankari id!");

                    if (!stateMachine.IsHero1Selected())
                    {
                        hero1 = heroes[idIndex];
                        stateMachine.SetHeroOne();
                    }
                    else if (!stateMachine.IsHero2Selected()) { 
                        hero2 = heroes[idIndex];
                        stateMachine.SetHeroTwo();
                    }
                } // go to selection confirmation screen
                if (!succes) await ErrorScreen("Virheellinen sankari id!");
                Console.Clear();
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
