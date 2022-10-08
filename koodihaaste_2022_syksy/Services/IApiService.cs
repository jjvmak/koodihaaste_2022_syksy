using koodihaaste_2022_syksy.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy.Services
{
    public interface IApiService
    {
        public Task<List<ItemDTO>> Process(string itemName);
    }
}
