using Core.SchoolMeal.Model.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.SchoolMeal.Model
{
    class MealServiceDietInfo 
    {
        [JsonProperty("head")]
        public List<Head> Head { get; set; }

        [JsonProperty("row")]
        public List<Row> row { get; set; }
    }
}
