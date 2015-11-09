﻿using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Windtalker.Domain.Exceptions;
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
            Post["/room"] = _ =>
            {
                var dto = this.Bind<RoomDto>();
                try
                {
                    var newRoom = createRoom.Create(dto.Name);
                    return new JsonObjectResponse(newRoom);
                }
                catch (RoomNameIsAlreadyTakenException)
                {
                    return ErrorResponse.FromMessage("Room name is already taken", HttpStatusCode.BadRequest);
                }
            };
        }
    }
}