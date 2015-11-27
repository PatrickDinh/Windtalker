(function () {
    function addRoomController($scope, $rootScope, roomService, bus, events) {
        var self = this;

        self.$inject = ["$scope", "roomService"];
        self.model = {};

        self.addRoom = function() {
            roomService.addRoom(self.model).success(function(response) {
                toastr.success("The room was created.");
                bus.publish(events.roomCreated, response);
                $scope.$parent.$close();
            });
        }

        self.cancel = function() {
            $scope.$parent.$dismiss();
        }
    }

    app.directive("addRoom", function () {
        return {
            restrict: "E",
            scope: {},
            controllerAs: "vm",
            bindToController: true,
            controller: addRoomController,
            templateUrl: "add-room.tpl.html"
        }
    });
})();