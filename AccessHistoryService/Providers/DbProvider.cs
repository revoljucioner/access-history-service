using AccessHistoryService.Contracts;
using AccessHistoryService.Models.DataModels;
using AccessHistoryService.Models.Enum;
using AccessHistoryService.Models.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessHistoryService.Providers
{
    public class DbProvider
    {
        private readonly Func<DataContext> _dbContextFunc;

        public DbProvider(IConfiguration configuration)
        {
            _dbContextFunc = new Func<DataContext>(() => new DataContext(configuration["ConnectionStrings:AccessDatabase"]));
        }
        public async Task<IEnumerable<GetDepartmentEventsCountItemResponse>> GetDepartmentEventsCount(EventType eventType, DateTime timeFrom, DateTime timeTo)
        {
            using (var context = _dbContextFunc())
            {
                var result = (from ev in context.Event
                         join emp in context.Employee
                            on ev.EmployeeId equals emp.Id
                         join dep in context.Department.DefaultIfEmpty()
                            on emp.DepartmentId equals dep.Id
                         where ev.EventTypeId == eventType 
                            && ev.Time.TimeOfDay > timeFrom.TimeOfDay
                            && ev.Time.TimeOfDay < timeTo.TimeOfDay
                         group dep by dep.Id into g
                         orderby g.Count() descending
                         select new GetDepartmentEventsCountItemResponse
                         {
                             Info = (DepartmentInfo)g.First(),
                             Count = g.Count()
                         })
                         .OrderByDescending(i => i.Count)
                         .ToArray();

                return result;
            }
        }

        public async Task<GetRoomEventsCountResponse> GetRoomEventsCount(Guid employeeId, EventType eventType)
        {
            using (var context = _dbContextFunc())
            {
                var maxVisitsByRoom = context.Event
                    .Where(e => e.EmployeeId == employeeId && e.EventTypeId == eventType)
                    .GroupBy(e => e.RoomId)
                    .Select(i => i.Count());

                if (maxVisitsByRoom.Count() == 0)
                    return new GetRoomEventsCountResponse { RoomInfos = Enumerable.Empty<RoomInfo>()};

                var maxVisitCount = maxVisitsByRoom.Max();

                var mostVisitedRooms = context.Event
                    .Where(e => e.EmployeeId == employeeId && e.EventTypeId == eventType)
                    .GroupBy(e => e.RoomId)
                    .Select(group => new 
                    { 
                        RoomId = group.Key, 
                        Count = group.Count() 
                    })
                    .Where(i => i.Count == maxVisitCount)
                    .Select(i => i.RoomId)
                    .Join(context.Room,
                        roomId => roomId,
                        room => room.Id,
                        (_, room) => room)
                    .Select(i => (RoomInfo)i)
                    .ToArray();

                return new GetRoomEventsCountResponse
                {
                    RoomInfos = mostVisitedRooms,
                    Count = maxVisitCount
                };
            }
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeEvents(Guid roomId, EventType eventType, DateTime timeFrom, DateTime timeTo)
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Event
                    .Where(@event => @event.RoomId == roomId &&
                                @event.EventTypeId == eventType 
                                && @event.Time > timeFrom
                                && @event.Time < timeTo)
                    .Join(context.Employee, 
                        @event => @event.EmployeeId,
                        employee => employee.Id,
                        (_, employee) => employee)
                    .Distinct()
                    .Select(i => (EmployeeInfo)i)
                    .ToArray();

                return result;
            }
        }
    }
}
