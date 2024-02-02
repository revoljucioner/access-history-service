using AccessManager.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Contracts
{
    public interface IEmployeeProvider
    {
        Task<Guid> AddEmployee(EmployeeInfo info);

        Task<EmployeeInfo> GetEmployee(Guid id);

        Task<IEnumerable<EmployeeInfo>> GetEmployees();
    }
}
