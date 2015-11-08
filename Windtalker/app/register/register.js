(function () {

    var controller = function ($http) {
        var self = this;
        self.$inject = ["$http"];
        self.registered = false;

        self.submit = function () {
            $http.post("/register", self.model)
                .success(function () {
                    self.registered = true;
                    toastr.success('Thank you for registering!');
                });
        }
    };

    addAngularState("register", "/register", "Register", controller, "");
})();


