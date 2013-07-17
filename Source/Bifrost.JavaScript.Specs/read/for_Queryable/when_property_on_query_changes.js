describe("when property on query changes", function () {

    var query = {
        someProperty: ko.observable(),

        areAllParametersSet: function () {
            return true;
        }

    };
    var paging = {
        size : 0,
        number : 0
    };

    var pagingInfoType = null;

    var queryService = null;

    beforeEach(function () {
        pagingInfoType = Bifrost.read.PagingInfo;

        Bifrost.read.PagingInfo = {
            create: function () {
                return paging;
            }
        };

        queryService = {
            execute: sinon.mock().withArgs(query, paging).once()
        };

        var instance = Bifrost.read.Queryable.create({
            query: query,
            queryService: queryService,
            targetObservable: {}
        });

        query.someProperty(42);
    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });
    

    it("should execute the query on the query service", function () {
        expect(queryService.execute.verify()).toBe(true);
    });
});