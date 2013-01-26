Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function (queryService) {
        var self = this;
        this.name = "";
        this.queryService = queryService;

        var queryables = {};

        this.target = this;

        function createQueryable() {
            var observable = ko.observableArray();
            observable.execute = function() {
                self.queryService.execute(self.target);
            };
            return observable;
        }

        function observeProperties(query) {
            for( var property in query ) {
                if( ko.isObservable(query[property]) == true ) {
                    query[property].subscribe(function() {
                        for( var queryable in queryables ) {
                            queryables[queryable].execute();
                        }
                    });
                }
            }
        }

        this.all = function () {
            if( typeof queryables.all === "undefined") queryables.all = createQueryable();
            queryables.all.execute();
            return queryables.all;
        };

        this.onCreated = function(query) {
            self.target = query;
            observeProperties(query);
        };
    })
});