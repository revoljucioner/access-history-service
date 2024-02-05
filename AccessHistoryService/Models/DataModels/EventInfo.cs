using AccessHistoryService.Models.Enum;
using System;

namespace AccessHistoryService.Models.DataModels
{
    public class EventInfo
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid RoomId { get; set; }

        public EventType EventTypeId { get; set; }

        public DateTime Time { get; set; }
    }
}
