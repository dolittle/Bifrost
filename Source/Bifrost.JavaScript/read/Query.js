Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function (queryService) {
        var self = this;
        this.name = "";
        this.queryService = queryService;
        this.queryables = {};

        this.completeCallbacks = [];

        this.currentQuery = 0;

        this.target = this;

        function createQueryable() {
            var observable = ko.observableArray();
            observable.execute = function () {
                var query = function (queryNumber) {
                    var queryNumber = queryNumber;
                    self.queryService.execute(self.target).continueWith(function (data) {
                        if (queryNumber == self.currentQuery) {
                            observable(data);
                            self.onComplete(data);
                        }
                    });
                }

                self.currentQuery = self.currentQuery+1;
                new query(self.currentQuery);
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

        this.complete = function (callback) {
            self.completeCallbacks.push(callback);
            return self;
        };

        this.onComplete = function (data) {
            $.each(self.completeCallbacks, function (index, callback) {
                callback(data);
            });
        };



        this.execute = function () {
            for (var queryable in self.queryables) {
                self.queryables[queryable].execute();
            }
        };

        this.all = function (execute) {
            if (typeof execute === "undefined") execute = true;
            if (typeof self.queryables.all === "undefined") self.queryables.all = createQueryable();
            if (execute === true) {
                self.queryables.all.execute();
            }
            return self.queryables.all;
        };

        this.onCreated = function (query) {
            self.target = query;
            observeProperties(query);
        };
    })
});