Bifrost.namespace("Bifrost.read", {
    queryableFactory: Bifrost.Singleton(function () {
        this.create = function (query, region) {
            var queryable = Bifrost.read.Queryable.new({
                query: query
            }, region);
            return queryable;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.queryableFactory = Bifrost.interaction.queryableFactory;