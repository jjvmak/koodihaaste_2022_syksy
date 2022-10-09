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
        // ================================

        // ========== Creation ============
        public HeroModel()
        {
            Name = "";
            Stats = Stats.Empty();
        }

        public HeroModel(string name, Stats stats)
        {
            Name = name;
            Stats = stats;
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
}
