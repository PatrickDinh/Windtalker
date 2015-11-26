(function () {
    function addRoomController($scope, $rootScope, roomService) {
        var self = this;

        self.$inject = ["$scope", "roomService"];
        self.model = {};

        self.addRoom = function() {
            roomService.addRoom(self.model).success(function(response) {
                toastr.success("The room was created.");
                $rootScope.$broadcast("room_created", response);
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