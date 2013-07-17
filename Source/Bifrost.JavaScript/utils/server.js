Bifrost.namespace("Bifrost", {
    server: Bifrost.Singleton(function () {
        var self = this;

        this.post = function (url, parameters) {
            var promise = Bifrost.execution.Promise.create();

            var actualParameters = {};

            for (var property in parameters) {
                actualParameters[property] = JSON.stringify(parameters[property]);
            }

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(actualParameters),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var data = $.parseJSON(result.responseText);
                    for (var property in data) {
                        data[property] = $.parseJSON(data[property]);
                    }
                    promise.signal(data);
                }
            });

            return promise;
        };

        this.get = function (url, parameters) {
            var promise = Bifrost.execution.Promise.create();

            $.ajax({
                url: url,
                type: "GET",
                dataType: 'json',
                data: parameters,
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var data = $.parseJSON(result.responseText);
                    for (var property in data) {
                        data[property] = $.parseJSON(data[property]);
                    }
                    promise.signal(data);
                }
            });

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.server = Bifrost.server;