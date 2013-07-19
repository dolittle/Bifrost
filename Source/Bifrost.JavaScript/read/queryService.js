Bifrost.namespace("Bifrost.read", {
    queryService: Bifrost.Singleton(function (server, readModelMapper) {
        var self = this;
        this.server = server;
        this.readModelMapper = readModelMapper;

        this.execute = function (query, paging) {
            var promise = Bifrost.execution.Promise.create();

            var url = "/Bifrost/Query/Execute?_q=" + query.generatedFrom;
            self.server.post(url, {
                descriptor: {
                    nameOfQuery: query.name,
                    generatedFrom: query.generatedFrom,
                    parameters: query.getParameterValues()
                },
                paging: {
                    size: paging.size,
                    number: paging.number
                }
            }).continueWith(function (result) {
                if (typeof result == "undefined" || result == null) {
                    result = {};
                }
                if (typeof result.items == "undefined" || result.items == null) result.items = [];
                if (typeof result.totalItems == "undefined" || result.totalItems == null) result.totalItems = 0;

                if (query.hasReadModel()) {
                    result.items = self.readModelMapper.mapDataToReadModel(query.readModel, result.items);
                }
                promise.signal(result);
            });

            return promise;
        };
    })
});