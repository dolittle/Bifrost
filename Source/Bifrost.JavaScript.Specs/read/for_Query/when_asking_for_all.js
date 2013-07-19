describe("when asking for all", function () {

    var query = Bifrost.read.Query.create();
    var queryable = null; 

    var queryableType;
    var executeStub = null;

    beforeEach(function () {
        queryableType = Bifrost.read.Queryable;

        executeStub = sinon.stub();
        queryable = {
            new: sinon.mock().withArgs({ query: query }).once().returns({
                execute: executeStub
            })
        };

        Bifrost.read.Queryable = queryable;

        query.all();
    });

    afterEach(function () {
        Bifrost.read.Queryable = queryableType;
    });

    it("should create a queryable and pass the query as parameter", function () {
        expect(queryable.new.verify()).toBe(true);
    });

    it("should not call execute on the queryable", function () {
        expect(executeStub.called).toBe(false);
    });
});