describe("when resolving", function () {
    beforeEach(function () {
        Bifrost.dependencyResolvers = {
            first: {
                canResolve: sinon.stub().returns(false)
            },
            second: {
                canResolve: sinon.stub().returns(false)
            }
        };
        Bifrost.dependencyResolver.resolve("Something");
    });

    afterEach(function () {
        Bifrost.dependencyResolvers = {};
    });

    it("should ask first resolver", function () {
        expect(Bifrost.dependencyResolvers.first.canResolve.called).toBe(true);
    });
    it("should ask second resolver", function () {
        expect(Bifrost.dependencyResolvers.second.canResolve.called).toBe(true);
    });
});