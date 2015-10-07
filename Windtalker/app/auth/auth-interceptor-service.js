"use strict";
app.service("authInterceptorService", ["$q", "$location", "localStorageService", function ($q, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get("authorizationData");
        if (authData) {
            config.headers.Authorization = authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            if ($location.path() !== "/login") {
                $location.path("/login");
            }
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);