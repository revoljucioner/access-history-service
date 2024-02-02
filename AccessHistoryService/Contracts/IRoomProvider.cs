using AccessManager.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessHistoryService.Contracts
{
    public interface IRoomProvider
    {
        Task<Guid> AddRoom(RoomInfo info);

        Task<RoomInfo> GetRoom(Guid id);

        Task<IEnumerable<RoomInfo>> GetRooms();
    }
}
