using AccessManager.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManager.Models.Database
{
    [Table("event")]
    public class EventDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("employee_id")]
        public Guid EmployeeId { get; set; }

        [Column("room_id")]
        public Guid RoomId { get; set; }

        [Column("event_type_id")]
        public EventType EventTypeId { get; set; }

        [Column("time")]
        public DateTime Time { get; set; }
    }
}
