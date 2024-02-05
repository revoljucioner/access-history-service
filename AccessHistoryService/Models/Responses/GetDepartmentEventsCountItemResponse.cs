using AccessHistoryService.Models.DataModels;
using Newtonsoft.Json;

namespace AccessHistoryService.Models.Responses
{
    public class GetDepartmentEventsCountItemResponse
    {
        [JsonProperty("info")]
        public DepartmentInfo Info { get; set; }

        [JsonProperty("cout")]
        public int Count { get; set; }
    }
}
