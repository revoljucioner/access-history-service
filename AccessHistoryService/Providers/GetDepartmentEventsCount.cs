using System;

namespace AccessHistoryService.Providers
{
    public class GetDepartmentEventsCount
    {
        public Guid DepartmentId {  get; set; }

        public string DepartmentName {  get; set; }

        public int Count {  get; set; }
    }

    public class GetRoomEventsCount
    {
        public Guid RoomId { get; set; }

        public string RoomName { get; set; }
    }

    public class GetEmployeeEvents
    {
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
