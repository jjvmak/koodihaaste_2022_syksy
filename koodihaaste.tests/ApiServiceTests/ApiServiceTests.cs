using koodihaaste_2022_syksy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace koodihaaste.tests.ApiServiceTests
{
    public class ApiServiceTests
    {
        private IApiService apiservice = new ApiService();

        [Fact]
        public async Task TestApi()
        {
            var dto = await apiservice.Process("porkkana");
            Assert.NotNull(dto);
        }
        
    }
}
