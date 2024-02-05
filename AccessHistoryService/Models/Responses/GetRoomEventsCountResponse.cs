using AccessHistoryService.Models.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AccessHistoryService.Models.Responses
{
    public class GetRoomEventsCountResponse
    {
        [JsonProperty("infos")]
        public IEnumerable<RoomInfo> RoomInfos { get; set; }

        [JsonProperty("cout")]
        public int Count { get; set; }
    }
}
