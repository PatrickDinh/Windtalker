"use strict";

app.service("chatHubService", ["$rootScope",
  function ($rootScope) {

    function backendFactory(serverUrl) {
      var connection = $.hubConnection("/signalr");
      var proxy = connection.createHubProxy(hubName);

      connection.start().done(function () { });

      return {
        on: function (eventName, callback) {
          proxy.on(eventName, function (result) {
            $rootScope.$apply(function () {
              if (callback) {
                callback(result);
              }
            });
          });
        },
        invoke: function (methodName, param, callback) {
          proxy.invoke(methodName)
          .done(function (result) {
            $rootScope.$apply(function () {
              if (callback) {
                callback(result);
              }
            });
          });
        }
      };
    };

    return backendFactory;
  }]);