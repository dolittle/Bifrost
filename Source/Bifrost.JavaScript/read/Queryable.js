Bifrost.namespace("Bifrost.read", {
    Queryable: Bifrost.Type.extend(function (query, queryService, region, targetObservable) {
        var self = this;

        this.canExecute = true;

        this.target = targetObservable;
        this.query = query;
        this.queryService = queryService;
        this.pageSize = ko.observable(0);
        this.pageNumber = ko.observable(0);
        this.totalItems = ko.observable(0);
        this.completedCallbacks = [];

        this.pageSize.subscribe(function () {
            if (self.canExecute) {
                self.execute();
            }
        });

        this.pageNumber.subscribe(function () {
            if (self.canExecute) {
                self.execute();
            }
        });

        function observePropertiesFrom(query) {
            for (var property in query) {
                if (ko.isObservable(query[property]) == true && query.hasOwnProperty(property) && property != "areAllParametersSet" ) {
                    query[property].subscribe(function () {
                        self.execute();
                    });
                }
            }
        }
        


        this.completed = function (callback) {
            self.completedCallbacks.push(callback);
            return self;
        };

        this.onCompleted = function (data) {
            self.completedCallbacks.forEach(function (callback) {
                callback(data);
            });
        };

        this.execute = function () {
            if (self.query.areAllParametersSet() !== true) {
                // TODO: Diagnostics - warning
                return self.target;
            }
            self.query._previousAreAllParametersSet = true;

            var paging = Bifrost.read.PagingInfo.create({
                size: self.pageSize(),
                number: self.pageNumber()
            });
            self.queryService.execute(query, paging).continueWith(function (result) {
                self.totalItems(result.totalItems);
                self.target(result.items);
                self.onCompleted(result.items);
            });

            return self.target;
        };

        this.setPageInfo = function (pageSize, pageNumber) {
            self.canExecute = false;
            self.pageSize(pageSize);
            self.pageNumber(pageNumber);
            self.canExecute = true;
            self.execute();
        };


        observePropertiesFrom(query);
        if (typeof self.query.areAllParametersSet.subscribe == "function") {

            self.query.areAllParametersSet.subscribe(function (isSet) {
                var shouldConsiderExecuting = true;
                if (!Bifrost.isNullOrUndefined(self.query._previousAreAllParametersSet)) {
                    if (self.query._previousAreAllParametersSet == isSet) {
                        shouldConsiderExecuting = false;
                    }
                }
                if (shouldConsiderExecuting == true) {
                    if (isSet === true) self.execute();
                }
            });
        }
    })
});
Bifrost.read.Queryable.new = function (options, region) {
    var observable = ko.observableArray();
    options.targetObservable = observable;
    options.region = region;
    var queryable = Bifrost.read.Queryable.create(options);
    Bifrost.extend(observable, queryable);
    observable.isQueryable = true;
    return observable;
};


