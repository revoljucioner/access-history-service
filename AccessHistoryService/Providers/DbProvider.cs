﻿using AccessHistoryService.Contracts;
using AccessManager.Models.Database;
using AccessManager.Models.DataModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessHistoryService.Providers
{
    public class DbProvider: IEmployeeProvider, IDepartmentProvider, IRoomProvider, IEventProvider
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
    }
}