using koodihaaste_2022_syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace koodihaaste.tests.ModelTests
{
    public class HeroModelTests
    {
        private HeroModel porkkana;
        private HeroModel paprika;

        public HeroModelTests()
        {
            this.porkkana = new HeroModel()
            {
                Name = "porkkana",
                Stats = new Stats()
                {
                    Health = 33,
                    Attack = 5.6,
                    Defence = 0.6,
                    Delay = 6.4
                }
            };

            this.paprika = new HeroModel()
            {
                Name = "paprika",
                Stats = new Stats()
                {
                    Health = 30,
                    Attack = 6.0,
                    Defence = 1.0,
                    Delay = 7.3
                }
            };
        }

        [Fact]
        public void TestDamageToEnemy()
        {
            var damageToPaprika = porkkana.DamageToEnemy(paprika.Stats.Defence);
            Assert.Equal(5.54 , damageToPaprika);

            paprika.Stats.Health = paprika.HealthAfterAttack(damageToPaprika);
            Assert.Equal(24.46, paprika.Stats.Health);

        }

        [Fact]
        public void TestIsHeroStillAlive()
        {
            porkkana.Stats.Health = 1.0;
            Assert.True(porkkana.StillAlive());

            porkkana.Stats.Health = 0.0;
            Assert.False(porkkana.StillAlive());

            porkkana.Stats.Health = -1.0;
            Assert.False(porkkana.StillAlive());

        }

        [Fact]
        public void ShouldBeAbleToAttackOnlyWhenDelayTimeIsUp()
        {
            // No attack yet so last attack time should be 0.0
            Assert.False(this.paprika.IsAbleToAttack(0.0));
            Assert.Equal(0.0, this.paprika.lastAttackTime);

            // Perform attack and set new last attack time
            Assert.True(this.paprika.IsAbleToAttack(7.3));
            paprika.lastAttackTime = 7.3;
            Assert.Equal(7.3, this.paprika.lastAttackTime);
            Assert.False(this.paprika.IsAbleToAttack(8.0));



        }

    }
}
