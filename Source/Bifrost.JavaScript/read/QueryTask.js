Bifrost.namespace("Bifrost.read", {
    QueryTask: Bifrost.tasks.LoadTask.extend(function (query, paging, taskFactory) {
        var url = "/Bifrost/Query/Execute?_q=" + query.generatedFrom;
        var payload = {
            descriptor: {
                nameOfQuery: query.name,
                generatedFrom: query.generatedFrom,
                parameters: query.getParameterValues()
            },
            paging: {
                size: paging.size,
                number: paging.number
            }
        };

        this.query = query.name;
        this.paging = payload.paging;

        var innerTask = taskFactory.createHttpPost(url, payload);

        this.execute = function () {
            var promise = innerTask.execute();
            return promise;
        };
    })
});