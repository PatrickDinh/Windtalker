(function () {

  var controller = function (currentUserService) {
    var self = this;
    self.$inject = ["currentUserService"];
    self.model = {};

    currentUserService.getCurrentUser();
  };

  addAngularState("edit-detail", "/editDetail", "Edit Detail", controller, "");
})();


