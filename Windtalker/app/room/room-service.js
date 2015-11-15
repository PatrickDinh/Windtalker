(function() {
    app.service("roomService", [
        "$http", function($http) {
            var roomService = {};

            roomService.getRooms = function() {
                return $http.get("/rooms");
            }

            return roomService;
        }
    ])

})();