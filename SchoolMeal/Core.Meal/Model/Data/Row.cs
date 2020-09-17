using Newtonsoft.Json;

namespace Core.Meal.Model.Data
{
    class Row
    {
        [JsonProperty("MMEAL_SC_NM")]
        public string MMEAL_SC_NM { get; set; }

        [JsonProperty("DDISH_NM")]
        public string DDISH_NM { get; set; }
    }
}
