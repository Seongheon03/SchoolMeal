using Meal_Parsing.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Parsing.Model
{
    class MealServiceDietInfo 
    {
        [JsonProperty("head")]
        public List<Head> Head { get; set; }

        [JsonProperty("row")]
        public List<Row> row { get; set; }
    }
}
