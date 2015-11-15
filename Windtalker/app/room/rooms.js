(function() {
    function roomsController($rootScope, roomService) {
        var self = this;
        self.rooms = [];

        function broadcastRoomChange(roomId) {
            $rootScope.$broadcast("chat_changeRoom", roomId);
        }

        function init() {
            roomService.getRooms().then(function (response) {
                self.rooms = response.data;

                // Set General as default room
                self.rooms.forEach(function(room) {
                    if (room.name === "General") {
                        self.selectedRoomId = room.id;
                        broadcastRoomChange(room.id);
                    }
                });
            });
        }

        self.changeRoom = function(roomId) {
            if (roomId === self.selectedRoomId) {
                return;
            }

            self.selectedRoomId = roomId;
            broadcastRoomChange(roomId);
        }

        init();
    }

    app.directive("rooms", function() {
        return {
            restrict: "E",
            scope: {},
            controllerAs: "vm",
            bindToController: true,
            controller: roomsController,
            templateUrl: "rooms.tpl.html"
        }
    });
})();