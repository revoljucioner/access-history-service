using AccessManager.Models.Database;
using AccessManager.Models.Requests.AccessHistory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Providers
{
    public class AccessHistoryManager
    {
        private readonly DbProvider _dbProvider;

        public AccessHistoryManager(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<Guid> AddEmployee(AddEmployeeRequest request)
        {
            var info = new EmployeeInfo
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,  
                LastName = request.LastName,
                DepartmentId = request.DepartmentId
            };

            await _dbProvider.AddEmployee(info);

            return info.Id;
        }

        public async Task<Guid> AddRoom(AddRoomRequest request)
        {
            var info = new RoomInfo
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _dbProvider.AddRoom(info);

            return info.Id;
        }

        public async Task<Guid> AddDepartment(AddDepartmentRequest request)
        {
            var info = new DepartmentInfo
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _dbProvider.AddDepartment(info);

            return info.Id;
        }

        public async Task<Guid> AddEvent(AddEventRequest request)
        {
            var info = new EventInfo
            {
                Id = Guid.NewGuid(),
                EmployeeId = request.EmployeeId,
                EventTypeId = request.EventTypeId,
                RoomId = request.RoomId,
                Time = request.Time
            };

            await _dbProvider.AddEvent(info);

            return info.Id;
        }

        public async Task<IEnumerable<EventInfo>> GetEvents(GetEventsRequest request)
        {
            var result = await _dbProvider.GetEvents();

            return result;
        }
    }
}
