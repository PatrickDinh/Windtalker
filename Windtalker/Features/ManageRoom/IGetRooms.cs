using System.Collections.Generic;
using Windtalker.Domain;

namespace Windtalker.Features.ManageRoom
{
    public interface IGetRooms
    {
        IEnumerable<Room> GetAllRooms();
    }
}