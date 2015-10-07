(function () {

    var controller = function ($scope, authService) {
        var self = this;
        self.messages = [];
        self.model = {};

        self.submit = function () {
            proxy.invoke("sendMessage", self.model.message);
        }

        self.addMessage = function (message) {
            self.messages.push(message);
            $scope.$apply();
        }

        var connection = $.hubConnection();
        connection.qs = {
            "token": authService.getToken()
        };
        var proxy = connection.createHubProxy("chatHub");
        proxy.on("displayMessage", function (message) {
            self.addMessage(message);
        });

        connection.start();
    };

    addAngularState("chat", "/chat", "chat", controller, "");
})();


