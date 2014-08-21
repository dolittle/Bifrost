Bifrost.namespace("Bifrost.read", {
    queryService: Bifrost.Singleton(function (mapper, taskFactory) {
        var self = this;

        this.execute = function (query, paging) {
            var promise = Bifrost.execution.Promise.create();
            var region = query.region;

            var task = taskFactory.createQuery(query, paging);
            region.tasks.execute(task).continueWith(function (result) {
                if (typeof result === "undefined" || result == null) {
                    result = {};
                }
                if (typeof result.items === "undefined" || result.items == null) {
                    result.items = [];
                }
                if (typeof result.totalItems === "undefined" || result.totalItems == null) {
                    result.totalItems = 0;
                }

                if (query.hasReadModel()) {
                    result.items = mapper.map(query.readModel, result.items);
                }
                promise.signal(result);
            });

            return promise;
        };
    })
});