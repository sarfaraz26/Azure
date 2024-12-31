using Newtonsoft.Json;

namespace HttpTrigger.Models
{
    public class Employee
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("isWorking")]
        public bool IsWorking {  get; set; }
    }
}
