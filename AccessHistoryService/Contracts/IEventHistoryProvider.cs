using AccessManager.Models.DataModels;
using AccessManager.Models.Enum;
using AccessManager.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Contracts
{
    public interface IEventHistoryProvider
    {
        Task<IEnumerable<GetDepartmentEventsCountItemResponse>> GetDepartmentEventsCount(EventType eventType, DateTime timeFrom, DateTime timeTo);

        Task<GetRoomEventsCountResponse> GetRoomEventsCount(Guid employeeId, EventType eventType);

        Task<IEnumerable<EmployeeInfo>> GetEmployeeEvents(Guid roomId, EventType eventType, DateTime timeFrom, DateTime timeTo);
    }
}
