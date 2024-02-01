using AccessManager.Models.Database;
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

        public async Task AddEmployee(EmployeeInfo info)
        {
            var dbModel = new EmployeeInfoDbModel
            {
                Id = info.Id,
                FirstName = info.FirstName,
                LastName = info.LastName,
                DepartmentId = info.DepartmentId
            };

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddRoom(RoomInfo info)
        {
            var dbModel = new RoomDbModel
            {
                Id = info.Id,
                Name = info.Name
            };

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddDepartment(DepartmentInfo info)
        {
            var dbModel = new DepartmentDbModel
            {
                Id = info.Id,
                Name = info.Name
            };

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddEvent(EventInfo info)
        {
            var dbModel = new EventDbModel
            {
                Id = info.Id,
                EmployeeId = info.EmployeeId,
                EventTypeId = info.EventTypeId,
                RoomId = info.RoomId,
                Time = info.Time
            };

            using (var context = _dbContextFunc())
            {
                context.Add(dbModel);

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EventInfo>> GetEvents()
        {
            using (var context = _dbContextFunc())
            {
                var result = context.Event.Select(i => new EventInfo
                {
                    Id = i.Id,
                    EmployeeId = i.EmployeeId,
                    EventTypeId = i.EventTypeId,
                    RoomId = i.RoomId,
                    Time = i.Time
                });

                return result.ToArray();
            }
        }
    }
}
