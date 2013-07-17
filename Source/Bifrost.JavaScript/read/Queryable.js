Bifrost.namespace("Bifrost.read", {
    Queryable: Bifrost.Type.extend(function (query, queryService, targetObservable) {
        var self = this;

        this.target = targetObservable;
        this.query = query;
        this.queryService = queryService;
        this.pageSize = ko.observable(0);
        this.pageNumber = ko.observable(0);
        this.completedCallbacks = [];

        this.pageSize.subscribe(function () {
            self.execute();
        });

        this.pageNumber.subscribe(function () {
            self.execute();
        });

        function observePropertiesFrom(query) {
            for (var property in query) {
                if (ko.isObservable(query[property]) == true) {
                    query[property].subscribe(function () {
                        self.execute();
                    });
                }
            }
        }

        observePropertiesFrom(query);

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
                return;
            }

            var clauses = Bifrost.read.Clauses.create({
                pageSize: self.pageSize(),
                pageNumber: self.pageNumber
            });
            self.queryService.execute(query, clauses).continueWith(function (items) {
                self.target(items);
                self.onCompleted(items);
            });
        };

        this.setPageInfo = function (pageSize, pageNumber) {
            self.pageSize(pageSize);
            self.pageNumber(pageNumber);
        };
    })
});
Bifrost.read.Queryable.new = function (options) {
    var observable = ko.observableArray();
    options.targetObservable = observable;
    var queryable = Bifrost.read.Queryable.create(options);
    Bifrost.extend(observable, queryable);
    return observable;
};


