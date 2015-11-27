(function() {
    function roomsController($rootScope, $uibModal, $scope, roomService, bus, events) {
        var self = this;
        self.$inject = ["$rootScope", "$uibModal", "$scope", "roomService", "bus", "events"];
        self.rooms = [];

        function broadcastRoomChange(roomId) {
            bus.publish(events.chatChangeRoom, roomId);
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

            bus.subscribe(events.roomCreated, function(e, args) {
                self.addRoom(args);
            }, $scope);
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