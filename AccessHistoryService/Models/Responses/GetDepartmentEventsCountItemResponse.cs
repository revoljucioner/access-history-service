using AccessManager.Models.DataModels;
using Newtonsoft.Json;

namespace AccessManager.Models.Responses
{
    public class GetDepartmentEventsCountItemResponse
    {
        [JsonProperty("info")]
        public DepartmentInfo Info { get; set; }

        [JsonProperty("cout")]
        public int Count { get; set; }
    }
}
