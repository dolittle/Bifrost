describe("when changing page size and all parameters are not set on the query", function () {

    var query = {
        someProperty: ko.observable(),
        areAllParametersSet: function () {
            return false;
        }
    };
    var clauses = {
        pageSize: 0,
        pageNumber: 0
    };

    var clausesType = null;

    var queryService = null;

    beforeEach(function () {
        clausesType = Bifrost.read.Clauses;

        Bifrost.read.Clauses = {
            create: function () {
                return clauses;
            }
        };

        queryService = {
            execute: sinon.mock().withArgs(query, clauses).never()
        };

        var instance = Bifrost.read.Queryable.create({
            query: query,
            queryService: queryService,
            targetObservable: {}
        });

        instance.pageSize(5);
    });

    afterEach(function () {
        Bifrost.read.Clauses = clausesType;
    });


    it("should not execute the query on the query service", function () {
        expect(queryService.execute.verify()).toBe(true);
    });
});