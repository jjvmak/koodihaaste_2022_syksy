using koodihaaste_2022_syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy
{
    public class StateMachine
    {
        private ActionResult search { get; set; } = ActionResult.NonDetermined;
        private Initialized hero1 { get; set; } = Initialized.No;
        private Initialized hero2 { get; set; } = Initialized.No;
        private Initialized game { get; set; } = Initialized.No;

        // Status setters
        public ActionResult SetSearchResult(List<HeroModel> userModels)
        {
            this.search = userModels.Count > 0 ? ActionResult.Succes : ActionResult.Fail;
            return this.search;
        }

        public Initialized SetHeroOne()
        {
            Initialized initialized = Initialized.No;
            if (!CanSelectHero()) hero1 = Initialized.No;
            else
            {
                initialized = Initialized.Yes;
                hero1 = Initialized.Yes;
            }
            return initialized;
        }

        public Initialized SetHeroTwo()
        {
            Initialized initialized = Initialized.No;
            if (!CanSelectHero()) hero2 = Initialized.No;
            else
            {
                initialized = Initialized.Yes;
                hero2 = Initialized.Yes;
            }
            return initialized;
        }

        public Initialized SetGame()
        {
            Initialized initialized = Initialized.No;
            if (!CanCreateGame()) game = Initialized.No;
            else
            {
                initialized = Initialized.Yes;
                game = Initialized.Yes;
            }
            return initialized;
        }

        public void Reset()
        {
            game = Initialized.No;
            hero1 = Initialized.No;
            hero2 = Initialized.No;
            search = ActionResult.NonDetermined;
        }

        // Status checkers
        public bool CanSelectHero() => search == ActionResult.Succes;

        public bool CanCreateGame() => hero1 == Initialized.Yes && hero2 == Initialized.Yes;

        public bool CanStartFight() => game == Initialized.Yes;


    
        
    }
}

public enum Menu
{
    Main,
    Help,
    Search,
    NoSearchResult,
    Fight,
    Scores
}

public enum ActionResult
{
    Succes,
    Fail,
    NonDetermined
}

public enum Initialized
{
    Yes,
    No
}

