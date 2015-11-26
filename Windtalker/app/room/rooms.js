(function() {
    function roomsController($rootScope, $uibModal, roomService) {
        var self = this;
        self.$inject = ["$rootScope", "$uibModal", "roomService"];
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

            $rootScope.$on("room_created", function(e, args) {
                self.addRoom(args);
            });
        }

        self.addRoom = function(room) {
            self.rooms.push(room);
        }

        self.changeRoom = function(roomId) {
            if (roomId === self.selectedRoomId) {
                return;
            }

            self.selectedRoomId = roomId;
            broadcastRoomChange(roomId);
        }

        self.showAddRoomModal = function() {
            $uibModal.open({
                template: "<add-room></add-room>"
            });
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