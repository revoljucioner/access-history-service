using System;

namespace AccessHistoryService.Models.Responses
{
    public class GetEmployeeEvents
    {
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
