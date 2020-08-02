using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Parsing.Model.Data
{
    class Row
    {
        [JsonProperty("MMEAL_SC_NM")]
        public string MMEAL_SC_NM { get; set; }

        [JsonProperty("DDISH_NM")]
        public string DDISH_NM { get; set; }
    }
}
