describe("when execution is complete", function () {
    var items = [
        { firstItem: 1 },
        { secondItem: 2 }
    ];
    var query = {
        areAllParametersSet: function () {
            return true;
        }
    };
    var observable = ko.observableArray();
    var queryService = null;

    var pagingInfoType = null;


    var dataReceived = null;
    var dataInQueryable = null;

    var completeCallback = function (data) {
        dataInQueryable = observable();
        dataReceived = data;
    };

    var queryable;


    beforeEach(function () {
        pagingInfoType = Bifrost.read.PagingInfo;
        Bifrost.read.PagingInfo = {
            create: function () {
                return {};
            }
        };

        queryService = {
            execute: function () {
                return {
                    continueWith: function (callback) {
                        
                        callback({
                            items: items,
                            totalItems: 4
                        });
                    }
                }
            }
        };

        queryable = Bifrost.read.Queryable.create({
            query: query,
            queryService: queryService,
            targetObservable: observable
        });

        queryable.completed(completeCallback);

        queryable.execute();
    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });


    it("should call the complete callback", function () {
        expect(dataReceived).toBe(items);
    });

    it("should set the total items", function () {
        expect(queryable.totalItems()).toBe(4);
    });

    it("should have set the data into the observable at before calling the callback", function () {
        expect(dataInQueryable).toBe(items);
    });
});