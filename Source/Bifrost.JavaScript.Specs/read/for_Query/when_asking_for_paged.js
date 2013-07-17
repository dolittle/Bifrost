describe("when asking for paged", function () {

    var query = Bifrost.read.Query.create();

    var queryReceived = null;

    var queryable = {
        create: function (options) {
            queryReceived = options.query;

            return {
                pageSize: ko.observable(),
                pageNumber: ko.observable(),
            }

        },
    };

    var queryableType;

    var paged = null;

    beforeEach(function () {
        queryableType = Bifrost.read.Queryable;
        Bifrost.read.Queryable = queryable;

        paged = query.paged(2,5);
    });

    afterEach(function () {
        Bifrost.read.Queryable = queryableType;
    });

    it("should create a queryable and pass the query as parameter", function () {
        expect(queryReceived).toBe(query);
    });

    it("should pass the page size to the queryable", function () {
        expect(paged.pageSize()).toBe(2);
    });

    it("should pass the page number to the queryable", function () {
        expect(paged.pageNumber()).toBe(5);
    });
});