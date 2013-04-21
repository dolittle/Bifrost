describe("when creating two instances with specific scope returning same value", function () {
    var type = null;
    var firstInstance = null;
    var secondInstance = null;
    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = Bifrost.Type.extend(function () {
        }).scopeTo({
            getFor: function () {
                return "Something";
            }
        });
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