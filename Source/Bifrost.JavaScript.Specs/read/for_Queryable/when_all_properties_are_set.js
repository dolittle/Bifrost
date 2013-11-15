describe("when all properties are set", function () {
    var query = {
        areAllParametersSet: ko.observable(false)
    };
    var paging = {
        size : 0,
        number : 0
    };

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
            queryService: queryService,
            targetObservable: {}
        });
        
        query.areAllParametersSet(true);
    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });
    
    it("should execute the query on the query service", function () {
        expect(queryService.execute.called).toBe(true);
    });
});