"use strict";
app.service("authService", [
    "$http", "$q", "localStorageService", function($http, $q, localStorageService) {
        var authServiceFactory = {};

        var _logOut = function() {
            localStorageService.remove("authorizationData");
        };

        var _login = function(loginData) {
            var deferred = $q.defer();

            $http.post("/login", loginData).success(function(response) {
                localStorageService.set("authorizationData", { token: response.authToken });
                deferred.resolve(response);
            }).error(function(err, status) {
                if (status === 401) {
                    toastr.warning("Invalid username or password");
                }
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        };

        var _isLoggedIn = function() {
            return localStorageService.get("authorizationData") != null;
        }

        var _getToken = function() {
            var storage = localStorageService.get("authorizationData");
            if (storage) {
                return storage.token;
            } else {
                return null;
            }
        }


        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.getToken = _getToken;
        authServiceFactory.isLoggedIn = _isLoggedIn;

        return authServiceFactory;
    }
]);