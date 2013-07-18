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
            }).continueWith(function (data) {
                var actualData = data;
                if (typeof actualData == "undefined" || actualData == null) actualData = [];

                if (query.hasReadModel()) {
                    actualData = self.readModelMapper.mapDataToReadModel(query.readModel, actualData);
                }
                promise.signal(actualData);
            });

            return promise;
        };
    })
});