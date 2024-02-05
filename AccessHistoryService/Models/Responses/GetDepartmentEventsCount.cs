using System;

namespace AccessHistoryService.Models.Responses
{
    public class GetDepartmentEventsCount
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int Count { get; set; }
    }
}
