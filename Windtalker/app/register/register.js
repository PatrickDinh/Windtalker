(function () {

  var controller = function ($http) {
    var self = this;
    self.$inject = ["$http"];

    self.submit = function () {
      $http.post("/register", self.model).success(function() {
        toastr.success('Thank you for registering!');
      });
    }
  };

  addAngularState("register", "/register", "Register", controller, "");
})();


