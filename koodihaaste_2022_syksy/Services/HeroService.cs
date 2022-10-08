using koodihaaste_2022_syksy.Models;
using LanguageExt;
using static LanguageExt.Prelude;

namespace koodihaaste_2022_syksy.Services
{
    public class HeroService : IHeroService
    {
        private readonly IApiService apiService;

        public HeroService(IApiService apiService) 
        {
            this.apiService = apiService;
        }
        public async Task<List<HeroModel>> SearchHeroes(string searchTerm)
        {
            var maybeValidResult = Optional(await this.apiService.Process(searchTerm));
            return maybeValidResult
                .Some(result => result.Select(value => HeroModel.FromDTO(value)).ToList())
                .None(() => new List<HeroModel>());
        }
    }
}
