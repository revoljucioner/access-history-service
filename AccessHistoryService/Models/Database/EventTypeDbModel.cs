using AccessHistoryService.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessHistoryService.Models.Database
{
    [Table("event_type")]
    public class EventTypeDbModel
    {
        [Column("id")]
        public EventType Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
