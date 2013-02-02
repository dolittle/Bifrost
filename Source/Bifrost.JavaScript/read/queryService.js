Bifrost.namespace("Bifrost.read", {
    queryService: Bifrost.Singleton(function () {
        var self = this;

        function createDescriptorFrom(query) {
            var descriptor = {
                nameOfQuery: query.name,
                parameters: {}
            };

            for (var property in query) {
                if (ko.isObservable(query[property]) == true) {
                    descriptor.parameters[property] = query[property]();
                }
            }

            return descriptor;
        }


        this.execute = function (query) {
            var promise = Bifrost.execution.Promise.create();
            var descriptor = createDescriptorFrom(query);

            var methodParameters = {
                descriptor: JSON.stringify(descriptor)
            };

            $.ajax({
                url: "/Bifrost/Query/Execute",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(methodParameters),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var items = $.parseJSON(result.responseText);
                    promise.signal(items);
                }
            });

            return promise;
        }
    })
});