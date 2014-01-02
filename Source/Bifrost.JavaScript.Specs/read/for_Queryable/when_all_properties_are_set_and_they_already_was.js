describe("when all properties are set and they already was", function () {
    var query = {
        areAllParametersSet: ko.observable(false)
    };
    var paging = {
        size : 0,
        number : 0
    };

    var region = {};
    var pagingInfoType = null;
    var queryService = {
        execute: sinon.stub()
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
        
        query.areAllParametersSet(true);
        query.areAllParametersSet(true);
    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });
    
    it("should not execute the query on the query service", function () {
        expect(queryService.execute.calledOnce).toBe(true);
    });
});