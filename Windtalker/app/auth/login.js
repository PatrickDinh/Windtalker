(function() {

    var controller = function(authService, $location, $state, bus, events) {
        var self = this;
        self.$inject = ["authService", "$location", "$state", "bus", events];
        self.model = {};

        if (authService.isLoggedIn()) {
            $state.go("chat");
        }

        self.submit = function() {
            authService.login(self.model).then(function () {
                bus.publish(events.userLoggedIn);
                $state.go("chat");
            });
        };
    };

    addAngularState("login", "/login", "Login", controller, "");
})();