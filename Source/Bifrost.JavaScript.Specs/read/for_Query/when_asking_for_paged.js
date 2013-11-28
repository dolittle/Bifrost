describe("when asking for paged", function () {

    var queryableFactory = {};
    var region = { some: "region" };

    var query = Bifrost.read.Query.create({
        queryableFactory: queryableFactory,
        region: region
    });

    var queryReceived = null;
    var regionReceived = null;
    var setPageInfoMock = null;

    queryableFactory.create = function (query, region) {
        queryReceived = query;
        regionReceived = region;

        setPageInfoMock = sinon.mock().withArgs(2, 5).once();
        return {
            setPageInfo: setPageInfoMock
        }
    };

    var paged = query.paged(2,5);

    it("should create a queryable and pass the query as parameter", function () {
        expect(queryReceived).toBe(query);
    });

    it("should create a queryable and pass the region as parameter", function () {
        expect(regionReceived).toBe(region);
    });

    it("should set the pageinfo on the queryable", function () {
        expect(setPageInfoMock.verify()).toBe(true);
    });
});