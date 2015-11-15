var app = angular.module("windtalker", [
  "ui.router",
  "LocalStorageModule"
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