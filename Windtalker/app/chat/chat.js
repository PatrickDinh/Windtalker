(function () {

    var controller = function ($scope, $rootScope, authService) {
        var self = this;
        self.messages = [];
        self.model = {};
        self.selectedRoomId = undefined;
        self.connectionReady = false;

        self.submit = function () {
            var message = {
                roomId: self.selectedRoomId,
                body: self.model.message
            }

            proxy.invoke("sendMessage", message);
        }

        self.addMessage = function (message) {
            if (message.roomId === self.selectedRoomId) {
                self.messages.push(message);
                $scope.$apply();
            }
        }

        $rootScope.$on("chat_changeRoom", function (e, roomId) {
            self.selectedRoomId = roomId;

            if (self.connectionReady) {
                getMessagesOnInit();
            }
        });

        function getMessagesOnInit()
        {
            proxy.invoke("getMessages", self.selectedRoomId).done(function (result) {
                self.messages = result;

                $scope.$apply();
            });
        }

        var connection = $.hubConnection();
        connection.qs = {
            "token": authService.getToken()
        };
        var proxy = connection.createHubProxy("chatHub");
        proxy.on("displayMessage", function (message) {
            self.addMessage(message);
        });

        connection.start().done(function() {
            self.connectionReady = true;

            getMessagesOnInit();
        });
    };

    addAngularState("chat", "/chat", "chat", controller, "");
})();


