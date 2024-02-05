using AccessHistoryService.Contracts;
using AccessHistoryService.Models.Enum;
using AccessHistoryService.Models.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Providers
{
    public class DbProvider : IEventHistoryProvider
    {
        private readonly string _connectionString;

        public DbProvider(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:AccessDatabase"];
        }

        public async Task<IEnumerable<GetDepartmentEventsCount>> GetDepartmentEventsCount(EventType eventType, DateTime timeFrom, DateTime timeTo)
        {
            var commandText = "select department.id as department_id, department.name as department_name, count(events.id) as count"
                + " from [dbo].event as events"
                + " left join employee on events.employee_id = employee.id"
                + " left join department on employee.department_id = department.id"
                + " where ((events.event_type_id = @eventType) and (cast(events.time as time) > @timeFrom) and (cast(events.time as time) < @timeTo))"
                + " group by department.id, department.name"
                + " order by count desc";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@eventType", eventType);
                command.Parameters.AddWithValue("@timeFrom", timeFrom.TimeOfDay);
                command.Parameters.AddWithValue("@timeTo", timeTo.TimeOfDay);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                var list = new List<GetDepartmentEventsCount>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new GetDepartmentEventsCount
                        {
                            DepartmentId = reader.GetGuid(0),
                            DepartmentName = reader.GetString(1),
                            Count = reader.GetInt32(2)
                        };

                        list.Add(item);
                    }
                }

                connection.Close();

                return list;
            }
        }

        public async Task<IEnumerable<GetRoomEventsCount>> GetRoomEventsCount(Guid employeeId, EventType eventType)
        {
            var commandText = "declare @max_events int"
                + " set @max_events ="
                + " (select top 1 count(room_id) as count_events"
                + " from event as events"
                + " where events.employee_id = @employeeId and events.event_type_id = @eventType"
                + " group by room_id"
                + " order by count_events desc)"

                + " select room.id as room_id, room.name as room_name"
                + " from event as events"
                + " join room on events.room_id = room.id"
                + " where events.employee_id = @employeeId and events.event_type_id = @eventType"
                + " group by room.id, room.name"
                + " having count(events.id) = @max_events";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@employeeId", employeeId);
                command.Parameters.AddWithValue("@eventType", eventType);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                var list = new List<GetRoomEventsCount>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new GetRoomEventsCount
                        {
                            RoomId = reader.GetGuid(0),
                            RoomName = reader.GetString(1)
                        };

                        list.Add(item);
                    }
                }

                connection.Close();

                return list;
            }
        }

        public async Task<IEnumerable<GetEmployeeEvents>> GetEmployeeEvents(Guid roomId, EventType eventType, DateTime timeFrom, DateTime timeTo)
        {
            var commandText = "select distinct employee.id, employee.first_name, employee.last_name"
                + " from event as events"
                + " join employee on events.employee_id = employee.id"
                + " where events.room_id = @roomId"
                + " and events.time < @timeTo"
                + " and events.time > @timeFrom"
                + " and events.event_type_id = @eventType";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@roomId", roomId);
                command.Parameters.AddWithValue("@eventType", eventType);
                command.Parameters.AddWithValue("@timeFrom", timeFrom);
                command.Parameters.AddWithValue("@timeTo", timeTo);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                var list = new List<GetEmployeeEvents>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new GetEmployeeEvents
                        {
                            EmployeeId = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        };

                        list.Add(item);
                    }
                }

                connection.Close();

                return list;
            }
        }
    }
}
