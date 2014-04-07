describe("when all properties are set", function () {
    var query = {
        foo: ko.observable(),
        areAllParametersSet: function () { return false; }
    };
    var paging = {
        size : 0,
        number : 0
    };

    var region = {};
    var pagingInfoType = null;
    var queryService = {
        execute: sinon.mock().withArgs(query, paging).once().returns({
            continueWith: function () { }
        })
    };

    beforeEach(function () {
        pagingInfoType = Bifrost.read.PagingInfo;

        Bifrost.read.PagingInfo = {
            create: function () {
                return paging;
            }
        };

        var instance = Bifrost.read.Queryable.create({
            query: query,
            region: region,
            queryService: queryService,
            targetObservable: {}
        });

        query.areAllParametersSet = function () {return true};
        query.foo(42);
    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });
    
    it("should execute the query on the query service", function () {
        expect(queryService.execute.once().verify()).toBe(true);
    });
});