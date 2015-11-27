(function () {

    var controller = function (authService, $rootScope, bus, events) {
        var self = this;
        self.$inject = ["authService", "$location"];
        self.isLoggedOut = false;

        function active() {
            authService.logOut();
            bus.publish(events.userLoggedOut);
            self.isLoggedOut = true;
        }

        active();
    };

    addAngularState("logout", "/logout", "Logout", controller, "");
})();