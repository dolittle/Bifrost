Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function (queryService) {
        var self = this;
        this.name = "";
        this.queryService = queryService;
        this.queryables = {};

        this.completedCallbacks = [];

        this.currentQuery = 0;

        this.isQueryingEnabled = true;

        this.target = this;

        function createQueryable() {
            var observable = ko.observableArray();
            observable.execute = function () {
                if (self.isAllParametersSet() == false) return;

                var query = function (queryNumber) {
                    var queryNumber = queryNumber;
                    self.queryService.execute(self.target).continueWith(function (data) {
                        if (queryNumber == self.currentQuery) {
                            observable(data);
                            self.onCompleted(data);
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

        this.isAllParametersSet = function () {
            var isSet = false;
            var hasParameters = false;
            for (var property in self.target) {
                if (ko.isObservable(self.target[property]) == true) {
                    hasParameters = true;
                    if (typeof self.target[property]() != "undefined" && self.target[property]() != null) {
                        isSet = true;
                    }
                }
            }
            if (hasParameters == false) return true;

            return isSet;
        }

        this.completed = function (callback) {
            self.completedCallbacks.push(callback);
            return self;
        };

        this.onCompleted = function (data) {
            $.each(self.completedCallbacks, function (index, callback) {
                callback(data);
            });
        };


        this.execute = function () {
            if( self.isQueryingEnabled ) {
                for (var queryable in self.queryables) {
                    self.queryables[queryable].execute();
                }
            }
        };

        this.setParameters = function (parameters) {
            try {
                self.isQueryingEnabled = false;
                for (var property in parameters) {
                    if (self.target.hasOwnProperty(property) && ko.isObservable(self.target[property]) == true) {
                        self.target[property](parameters[property]);
                    }
                }
            } catch(ex) {}

            self.isQueryingEnabled = true;
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