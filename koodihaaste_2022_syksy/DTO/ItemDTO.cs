using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koodihaaste_2022_syksy.DTO
{
    public class ItemDTO
    {
        public NameDTO? name { get; set; }
        
        public double protein { get; set; }

        public double fat { get; set; }

        public double carbohydrate { get; set; }

        public double energy { get; set; }
    }

    public class NameDTO
    {
        public string? fi { get; set; }
        public string? sv { get; set; }
        public string? en { get; set; }
    }
}

