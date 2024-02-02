using AccessHistoryService.Contracts;
using AccessManager.Models.Database;
using AccessManager.Models.DataModels;
using AccessManager.Models.Enum;
using AccessManager.Models.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessHistoryService.Providers
{
    public class DbProvider : IEmployeeProvider, IDepartmentProvider, IRoomProvider, IEventProvider, IEventHistoryProvider
    {
        private readonly Func<DataContext> _dbContextFunc;

        public DbProvider(IConfiguration configuration)
        {
            _dbContextFunc = new Func<DataContext>(() => new DataContext(configuration["ConnectionStrings:AccessDatabase"]));
        }

        public async Task<Guid> AddEmployee(EmployeeInfo info)
        {
            var dbModel = (EmployeeInfoDbModel)info;

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }

            return dbModel.Id;
        }

        public async Task<EmployeeInfo> GetEmployee(Guid id)
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Employee.Single(i => i.Id == id);

                return (EmployeeInfo)result;
            }
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployees()
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Employee.Select(i => (EmployeeInfo)i);

                return result.ToArray();
            }
        }

        public async Task<Guid> AddRoom(RoomInfo info)
        {
            var dbModel = (RoomDbModel)info;

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }

            return dbModel.Id;
        }

        public async Task<RoomInfo> GetRoom(Guid id)
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Room.Single(i => i.Id == id);

                return (RoomInfo)result;
            }
        }

        public async Task<IEnumerable<RoomInfo>> GetRooms()
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Room.Select(i => (RoomInfo)i);

                return result.ToArray();
            }
        }

        public async Task<Guid> AddDepartment(DepartmentInfo info)
        {
            var dbModel = (DepartmentDbModel)info;

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }

            return dbModel.Id;
        }

        public async Task<DepartmentInfo> GetDepartment(Guid id)
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Department.Single(i => i.Id == id);

                return (DepartmentInfo)result;
            }
        }

        public async Task<IEnumerable<DepartmentInfo>> GetDepartments()
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Department.Select(i => (DepartmentInfo)i);

                return result.ToArray();
            }
        }

        public async Task<Guid> AddEvent(EventInfo info)
        {
            var dbModel = (EventDbModel)info;

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }

            return dbModel.Id;
        }

        public async Task<EventInfo> GetEvent(Guid id)
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Event.Single(i => i.Id == id);

                return (EventInfo)result;
            }
        }

        public async Task<IEnumerable<EventInfo>> GetEvents()
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Event.Select(i => (EventInfo)i);

                return result.ToArray();
            }
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
