(function() {

    var controller = function(authService, $location, $state, $rootScope) {
        var self = this;
        self.$inject = ["authService", "$location"];
        self.model = {};

        if (authService.isLoggedIn()) {
            $state.go("chat");
        }

        self.submit = function() {
            authService.login(self.model).then(function () {
                $rootScope.$broadcast("user_loggedIn");
                $state.go("chat");
            });
        };
    };

    addAngularState("login", "/login", "Login", controller, "");
})();