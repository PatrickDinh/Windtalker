var app = angular.module("windtalker", [
  "ui.router",
  "LocalStorageModule",
  "ui.bootstrap"
]);

app.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.when("", "/login")
                    .when("/", "/login")
                    .otherwise(() => alert("not found"));
}]);

app.config(["$httpProvider", function ($httpProvider) {
    $httpProvider.interceptors.push(function ($q) {
        return {
            'responseError': function (response) {
                if (response.status === 401) {
                    return $q.reject(response);
                }

                var error = (response.data && response.data.errorMessage || response.data.errors[0]) || response.statusText || "An error occured";
                toastr.error(error);
                return $q.reject(response);
            }
        };
    });
    $httpProvider.interceptors.push("authInterceptorService");

    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);

app.run(function (authService, $state) {
    if (!authService.isLoggedIn()) {
        $state.go("login");
    }
});

function addAngularState(id, url, title, controller, template) {
    template = template || (id + ".tpl.html");

    var stateConfig = {
        url: url,
        templateUrl: template,
        controller: controller,
        controllerAs: "vm",
        params: { title: title },
        bindToController: true
    };

    app.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state(id, stateConfig);
    }]);
}