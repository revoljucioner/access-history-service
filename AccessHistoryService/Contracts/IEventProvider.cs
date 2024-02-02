using AccessManager.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Contracts
{
    public interface IEventProvider
    {
        Task<Guid> AddEvent(EventInfo info);

        Task<EventInfo> GetEvent(Guid id);

        Task<IEnumerable<EventInfo>> GetEvents();
    }
}
