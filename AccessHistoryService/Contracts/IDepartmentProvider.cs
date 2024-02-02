using AccessManager.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Contracts
{
    public interface IDepartmentProvider
    {
        Task<Guid> AddDepartment(DepartmentInfo info);

        Task<DepartmentInfo> GetDepartment(Guid id);

        Task<IEnumerable<DepartmentInfo>> GetDepartments();
    }
}
