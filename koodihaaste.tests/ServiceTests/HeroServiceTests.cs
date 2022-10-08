using koodihaaste_2022_syksy.DTO;
using koodihaaste_2022_syksy.Models;
using koodihaaste_2022_syksy.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace koodihaaste.tests.ServiceTests
{
    public class HeroServiceTests
    {
        private Mock<IApiService> apiServiceMock;
        private HeroService heroService;
        private Random random = new Random();

        public HeroServiceTests()
        {
            this.apiServiceMock = new Mock<IApiService>();
            this.heroService = new HeroService(this.apiServiceMock.Object);
        }

        [Fact]
        public async Task ShouldGetHeroes()
        {
            // Arrange
            var searchTerm = "test";
            var dtos = createDTOs();
            this.apiServiceMock.Setup(x => x.Process(searchTerm)).Returns(Task.FromResult(dtos));

            // Act
            var result = await this.heroService.SearchHeroes(searchTerm);

            // Assert
            Assert.Equal(dtos.Count, result.Count);
            Assert.True(!String.IsNullOrEmpty(result[0].Name));
            Assert.True((result[0].Name.Length == 19));

        }

        [Fact]
        public async Task ShouldNotBreakOnEmptyResult()
        {
            // Arrange
            var searchTerm = "test";

            // Act
            var result = await this.heroService.SearchHeroes(searchTerm);

            // Assert
            Assert.Empty(result);
        }

        private List<ItemDTO> createDTOs()
        {
            return new List<ItemDTO>() { 
                createRandomItemDTO(),
                createRandomItemDTO(),
                createRandomItemDTO()
            };
        }

        private ItemDTO createRandomItemDTO()
        {
            return new ItemDTO()
            {
                protein = random.NextDouble(),
                carbohydrate = random.NextDouble(),
                energy = random.NextDouble(),
                fat = random.NextDouble(),
                name = RandomNames()
            };
        }

        private NameDTO RandomNames()
        {
            return new NameDTO()
            {
                en = RandomString(19),
                fi = RandomString(19),
                sv = RandomString(19)
            };
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
