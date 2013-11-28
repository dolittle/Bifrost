Bifrost.namespace("Bifrost.read", {
    queryService: Bifrost.Singleton(function (readModelMapper, taskFactory) {
        var self = this;

        this.execute = function (query, paging) {
            var promise = Bifrost.execution.Promise.create();
            var region = query.region;

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

            var task = taskFactory.createHttpPost(url,payload);
            region.tasks.execute(task).continueWith(function (result) {
                if (typeof result == "undefined" || result == null) {
                    result = {};
                }
                if (typeof result.items == "undefined" || result.items == null) result.items = [];
                if (typeof result.totalItems == "undefined" || result.totalItems == null) result.totalItems = 0;

                if (query.hasReadModel()) {
                    result.items = readModelMapper.mapDataToReadModel(query.readModel, result.items);
                }
                promise.signal(result);
            });

            return promise;
        };
    })
});