using koodihaaste_2022_syksy.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy.Services
{
    public class ApiService : IApiService
    {
        private readonly string ApiUrl = "https://fineli.fi/fineli/api/v1/foods?q=";
        private static readonly HttpClient client = new HttpClient();

        public ApiService()
        {

        }

        public async Task<List<ItemDTO>> Process(string itemName)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = await client.GetStreamAsync($"{ApiUrl}{itemName}");
            var dtos = await JsonSerializer.DeserializeAsync<List<ItemDTO>>(streamTask);
            return dtos != null ? dtos : new List<ItemDTO>();
        }
    }
}
