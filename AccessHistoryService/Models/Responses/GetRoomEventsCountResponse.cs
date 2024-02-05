using AccessManager.Models.DataModels;
using Newtonsoft.Json;

namespace AccessManager.Models.Responses
{
    public class GetRoomEventsCountResponse
    {
        [JsonProperty("infos")]
        public IEnumerable<RoomInfo> RoomInfos { get; set; }

        [JsonProperty("cout")]
        public int Count { get; set; }
    }
}
