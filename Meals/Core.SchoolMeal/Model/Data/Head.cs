using Newtonsoft.Json;

namespace Core.SchoolMeal.Model.Data
{
    class Head
    {
        [JsonProperty("list_total_count")]
        public int ListTotalCount { get; set; }

        [JsonProperty("RESULT")]
        public Result result { get; set; }
    }
}
