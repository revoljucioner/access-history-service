using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccessHistoryService.Models.Database
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
