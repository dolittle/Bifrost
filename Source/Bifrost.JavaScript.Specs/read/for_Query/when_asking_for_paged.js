describe("when asking for paged", function () {

    var query = Bifrost.read.Query.create();

    var queryReceived = null;
    var setPageInfoMock = null;

    var queryable = {
        new: function (options) {
            queryReceived = options.query;

            setPageInfoMock = sinon.mock().withArgs(2,5).once();
            return {
                setPageInfo: setPageInfoMock
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

    it("should set the pageinfo on the queryable", function () {
        expect(setPageInfoMock.verify()).toBe(true);
    });
});