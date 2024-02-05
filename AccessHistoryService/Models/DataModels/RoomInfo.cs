using AccessHistoryService.Models.Database;
using Newtonsoft.Json;

using System;
namespace AccessHistoryService.Models.DataModels
{
    public class RoomInfo
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public static explicit operator RoomInfo(RoomDbModel db) => new RoomInfo
        {
            Id = db.Id,
            Name = db.Name
        };
    }
}
