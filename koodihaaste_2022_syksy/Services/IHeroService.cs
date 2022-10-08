using koodihaaste_2022_syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy.Services
{
    public interface IHeroService
    {
        public Task<List<HeroModel>> SearchHeroes(string searchTerm);
    }
        
}
