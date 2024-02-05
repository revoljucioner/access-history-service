using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManager.Models.Database
{
    [Table("room")]
    public class RoomDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
