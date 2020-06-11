using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Parsing.Model.Data
{
    class Result
    {
        [JsonProperty("CODE")]
        public string Code { get; set; }

        [JsonProperty("MESSAGE")]
        public string Message { get; set; }
    }
}
