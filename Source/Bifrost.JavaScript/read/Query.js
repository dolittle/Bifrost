Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function (queryService) {
        var self = this;
        this.name = "";
        this.queryService = queryService;
        this.queryables = {};

        this.target = this;

        function createQueryable() {
            var observable = ko.observableArray();
            observable.execute = function () {
                self.queryService.execute(self.target).continueWith(function (data) {
                    observable(data);
                });
            };
            return observable;
        }

        function observeProperties(query) {
            for (var property in query) {
                if (ko.isObservable(query[property]) == true) {
                    query[property].subscribe(function () {
                        query.execute();
                    });
                }
            }
        }

        this.execute = function () {
            for (var queryable in self.queryables) {
                self.queryables[queryable].execute();
            }
        };

        this.all = function () {
            if (typeof self.queryables.all === "undefined") self.queryables.all = createQueryable();
            self.queryables.all.execute();
            return self.queryables.all;
        };

        this.onCreated = function (query) {
            self.target = query;
            observeProperties(query);
        };
    })
});