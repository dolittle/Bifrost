Bifrost.namespace("Bifrost.services", {
    Service: Bifrost.Type.extend(function () {
        var self = this;

        this.url = "";
        this.name = "";

        function prepareArguments(args) {
            var prepared = {};

            for (var property in args) {
                prepared[property] = JSON.stringify(args[property]);
            }

            var stringified = JSON.stringify(prepared);
            return stringified;
        }

        function call(method, args, callback) {
            $.ajax({
                url: self.url + "/" + method,
                type: 'POST',
                dataType: 'json',
                data: prepareArguments(args),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var v = $.parseJSON(result.responseText);
                    callback(v);
                }
            });
        }


        this.callWithoutReturnValue = function (method, args) {
            var promise = Bifrost.execution.Promise.create();
            call(method, args, function (v) {
                promise.signal();
            });
            return promise;
        };

        this.callWithObjectAsReturn = function (method, args) {
            var value = ko.observable();
            call(method, args, function (v) {
                value(v);
            });
            return value;
        };

        this.callWithArrayAsReturn = function (method, args) {
            var value = ko.observableArray();
            call(method, args, function (v) {
                value(v);
            });
            return value;
        };

        this.onCreated = function (lastDescendant) {
            self.url = lastDescendant.url;
            if (self.url.indexOf("/") !== 0) self.url = "/" + self.url;

            self.name = lastDescendant.name;
        };
    })
});