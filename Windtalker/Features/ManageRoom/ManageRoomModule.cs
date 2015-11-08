using System;
using System.Linq;
using Nancy;
using Windtalker.Plumbing;

namespace Windtalker.Features.ManageRoom
{
    public class ManageRoomModule : NancyModule
    {
        public ManageRoomModule(ICreateRoom createRoom, IGetRooms getRooms)
        {
            Get["/rooms"] = _ =>
            {
                var allRooms = getRooms.GetAllRooms();
                var dto = allRooms.Select(r => new RoomDto
                {
                    Id = r.Id,
                    Name = r.Name
                });
                return new JsonObjectResponse(dto);
            };
        }
    }

    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}