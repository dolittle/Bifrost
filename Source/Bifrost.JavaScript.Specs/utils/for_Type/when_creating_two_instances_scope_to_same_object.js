describe("when creating two instances scoped to same object", function () {
    var type = null;
    var firstInstance = null;
    var secondInstance = null;
    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = Bifrost.Type.extend(function () {
        }).scopeTo("something");
        firstInstance = type.create();
        secondInstance = type.create();
    });

    afterEach(function () {
        Bifrost.dependencyResolver = {};
    });

    it("should return same instances", function () {
        expect(firstInstance).toBe(secondInstance);
    });
});