using System.ComponentModel.DataAnnotations.Schema;

namespace AccessManager.Models.Database
{
    [Table("employee")]
    public class EmployeeInfoDbModel
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("department_id")]
        public Guid? DepartmentId { get; set; }
    }
}
