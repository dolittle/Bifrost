describe("when executing when we have one item and we get two items back", function () {
    var items = [
        { firstItem: 1 },
        { secondItem: 2 }
    ];
    var query = {
        areAllParametersSet: function () {
            return true;
        }
    };
    var observable = ko.observableArray([
        { initialItem: 0 }
    ]);
    var queryService = null;

    var pagingInfoType = null;

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
                        
                        callback(items);
                    }
                }
            }
        };

        var queryable = Bifrost.read.Queryable.create({
            query: query,
            queryService: queryService,
            targetObservable: observable
        });

        queryable.execute();

    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });

    it("should populate the target observable", function () {
        expect(observable()).toBe(items);
    });
});