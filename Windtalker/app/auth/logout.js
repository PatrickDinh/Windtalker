(function () {

    var controller = function (authService, $rootScope) {
        var self = this;
        self.$inject = ["authService", "$location"];
        self.isLoggedOut = false;

        function active() {
            authService.logOut();
            $rootScope.$broadcast("user_loggedOut");
            self.isLoggedOut = true;
        }

        active();
    };

    addAngularState("logout", "/logout", "Logout", controller, "");
})();