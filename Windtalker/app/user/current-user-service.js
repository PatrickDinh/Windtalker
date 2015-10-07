"use strict";
app.service("currentUserService", ["$http", "$q", "localStorageService", function ($http) {
  var currentUserService = {};

  currentUserService.getCurrentUser = function () {
    $http.get("/currentUser").success(function(data) {
      console.log(data);
    });
  }

  return currentUserService;
}]);