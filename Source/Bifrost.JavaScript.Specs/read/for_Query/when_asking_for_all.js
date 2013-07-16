describe("when asking for all", function () {

    var query = Bifrost.read.Query.create();
    var queryable = {
        create: sinon.mock().withArgs({ query: query }).once()
    };

    var queryableType;

    beforeEach(function () {
        queryableType = Bifrost.read.Queryable;
        Bifrost.read.Queryable = queryable;

        
        query.all();
    });

    afterEach(function () {
        Bifrost.read.Queryable = queryableType;
    });

    it("should create a queryable and pass the query as parameter", function () {
        expect(queryable.create.verify()).toBe(true);
    });
});