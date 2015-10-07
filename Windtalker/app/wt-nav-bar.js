(function () {
    "use strict";

    app.directive("wtNavBar", function () {
        return {
            controller: function (authService, $scope) {
                var self = this;

                self.isLoggedIn = authService.isLoggedIn();

                $scope.$on("user_loggedIn", function() {
                    self.isLoggedIn = true;
                });
                $scope.$on("user_loggedOut", function () {
                    self.isLoggedIn = false;
                });
            },
            controllerAs: "vm",
            bindToController: true,
            templateUrl: "wt-nav-bar.tpl.html"
        }
    });
})();