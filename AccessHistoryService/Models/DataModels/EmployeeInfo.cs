using AccessManager.Models.Database;

namespace AccessManager.Models.DataModels
{
    public class EmployeeInfo
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? DepartmentId { get; set; }

        public static explicit operator EmployeeInfo(EmployeeInfoDbModel db) => new EmployeeInfo
        {
            Id = db.Id,
            FirstName = db.FirstName,
            LastName = db.LastName,
            DepartmentId = db.DepartmentId
        };
    }
}
