using AccessHistoryService.Models.Enum;
using AccessHistoryService.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Contracts
{
    public interface IEventHistoryProvider
    {
        Task<IEnumerable<GetDepartmentEventsCount>> GetDepartmentEventsCount(EventType eventType, DateTime timeFrom, DateTime timeTo);

        Task<IEnumerable<GetRoomEventsCount>> GetRoomEventsCount(Guid employeeId, EventType eventType);

        Task<IEnumerable<GetEmployeeEvents>> GetEmployeeEvents(Guid roomId, EventType eventType, DateTime timeFrom, DateTime timeTo);
    }
}
