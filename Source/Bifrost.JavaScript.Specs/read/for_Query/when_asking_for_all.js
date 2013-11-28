describe("when asking for all", function () {

    var region = { some:"region"};

    var queryableFactory = {
    };

    var query = Bifrost.read.Query.create({
        queryableFactory: queryableFactory,
        region: region
    });

    queryableFactory.create = sinon.mock().withArgs(query, region).once().returns({
        execute: executeStub
    });

    var executeStub = sinon.stub();
    query.all();

    it("should create a queryable and pass the query as parameter", function () {
        expect(queryableFactory.create.called).toBe(true);
    });

    it("should not call execute on the queryable", function () {
        expect(executeStub.called).toBe(false);
    });
});