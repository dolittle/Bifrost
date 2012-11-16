describe("when resolving", function () {
    var firstResolver = {
        canResolve: sinon.stub().returns(false)
    };
    var secondResolver = {
        canResolve: sinon.stub().returns(false)
    };

    beforeEach(function () {
        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [firstResolver, secondResolver];
            }
        };
        try {
            Bifrost.dependencyResolver.resolve("Something");
        } catch (e) {
        }
    });

    afterEach(function () {
        Bifrost.dependencyResolvers = {};
    });

    it("should ask first resolver", function () {
        expect(firstResolver.canResolve.called).toBe(true);
    });
    it("should ask second resolver", function () {
        expect(secondResolver.canResolve.called).toBe(true);
    });
});