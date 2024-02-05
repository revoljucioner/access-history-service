using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManager.Models.Database
{
    [Table("department")]
    public class DepartmentDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
