using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Parsing.Model.Data
{
    class Head
    {
        [JsonProperty("list_total_count")]
        public int ListTotalCount { get; set; }

        [JsonProperty("RESULT")]
        public Result result { get; set; }
    }
}
