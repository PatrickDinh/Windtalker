(function () {
    "use strict";

    angular
        .module("windtalker")
        .factory("bus", bus);

    function bus($rootScope) {
        var service = {
            publish: publish,
            subscribe: subscribe
        };

        return service;

        function publish(topic, data) {
            if ($rootScope.$$phase) {
                $rootScope.$broadcast(topic, data || {});
            } else {
                $rootScope.$apply($rootScope.$broadcast(topic, data || {}));
            }
        }

        function subscribe(topic, callback, scope) {
            // If you directly bind to $rootScope.$on() from within a controller, you have to 
            // clean up the binding yourself when your local $scope gets destroyed. 
            // This is because controllers can get instantiated multiple times over the lifetime 
            // of an application which would result into bindings creating memory leaks.
            // In summary, subscribing in a controller == true, pass in scope

            var unbind = $rootScope.$on(topic, function (event, data) {
                callback(event, data);
            });

            if (scope) {
                scope.$on("$destroy", unbind);
            }
        }
    }
})();