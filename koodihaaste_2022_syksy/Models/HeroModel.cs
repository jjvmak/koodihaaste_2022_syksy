using koodihaaste_2022_syksy.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy.Models
{
    public class HeroModel
    {
        // ========== Properties ==========
        public string Name { get; set; }
        public Stats Stats { get; set; }
        public string Color { get; set; }
        public string Speciality { get; set; }
        public int AttackCount { get; set; } = 0;
        // ================================

        // ========== Creation ============
        public HeroModel()
        {
            Name = "";
            Stats = Stats.Empty();
            Color = "white";
        }

        public HeroModel(string name, Stats stats)
        {
            // Set speciality for her
            if (Speciality == null) {
                Random rng = new Random();
                var d3 = rng.Next(3) + 1;
                switch (d3)
                {
                    // Miekka
                    case 1:
                        stats.Attack = stats.Attack * 1.2;
                        Speciality = "Taikamiekka";
                        break;
                    // Kilpi
                    case 2:
                        stats.Defence = stats.Defence * 1.4;
                        Speciality = "Mithrilkilpi";
                        break;
                    // Kengät
                    case 3:
                        stats.Delay = stats.Delay * 0.8;
                        Speciality = "Nopeuskengät";
                        break;
                }
            }
            

            Name = name;
            Stats = stats;
            Color = "white";
        }

        private static string NameFromDTO(ItemDTO dto) => dto.name != null ? (dto.name.fi != null ? dto.name.fi : ""): "";
        public static HeroModel FromDTO(ItemDTO dto) => new HeroModel(NameFromDTO(dto), Stats.FromDTO(dto));
        // ==============================

       
        // ========== Action ============
        public double DamageToEnemy(double enemyDef) 
            => Math.Round(Stats.Attack * ((100 - enemyDef) / 100), 2);
        
        public double HealthAfterAttack(double enemyAttack) 
            => Math.Round(Stats.Health - enemyAttack, 2);
        
        public bool StillAlive() => Stats.Health > 0;
        // ==============================

        // ====== Attack and delay ========
        public double lastAttackTime { get; set; } = 0.0;
        public bool IsAbleToAttack(double gameTime) => gameTime - lastAttackTime >= Stats.Delay;

        // ================================


    }

    public class Stats
    {
        public double Health { get; set; }
        public double Attack { get; set; }
        public double Defence { get; set; }
        public double Delay { get; set; }
        public static Stats FromDTO(ItemDTO dto) =>
            new Stats()
            {
                Defence = dto.protein,
                Delay = dto.carbohydrate + dto.protein + dto.fat,
                Attack = dto.carbohydrate,
                Health = dto.energy
            };
       
        public static Stats Empty() => new Stats() { Defence = 0, Delay = 0, Attack = 0, Health = 0 };

    }

    public enum Speciality
    {
        Taikamiekka,
        Mithrilkilpi,
        Nopeuskengät,        
    }
    
}
