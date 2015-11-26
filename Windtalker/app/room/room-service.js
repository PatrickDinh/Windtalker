(function() {
    app.service("roomService", [
        "$http", function($http) {
            var roomService = {};

            roomService.getRooms = function() {
                return $http.get("/rooms");
            }

            roomService.addRoom = function(model) {
                return $http.post("/room", model);
            }

            return roomService;
        }
    ])

})();