describe("when creating two instances", function () {
    var type = null;
    var firstInstance = null;
    var secondInstance = null;
    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = Bifrost.Singleton(function () {
            this.something = "When creating two instances";
        });
        firstInstance = type.create();
        secondInstance = type.create();
    });

    afterEach(function () {
        Bifrost.dependencyResolver = {};
    });

    it("should return correct instance for the first", function () {
        expect(firstInstance.something).toBe("When creating two instances");
    });

    it("should return correct instance for the second", function () {
        expect(secondInstance.something).toBe("When creating two instances");
    });

});