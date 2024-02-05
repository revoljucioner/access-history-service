using System;

namespace AccessHistoryService.Models.Responses
{
    public class GetRoomEventsCount
    {
        public Guid RoomId { get; set; }

        public string RoomName { get; set; }
    }
}
