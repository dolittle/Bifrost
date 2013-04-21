describe("when creating two instances with specific scope returning null", function () {
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
                return null;
            }
        });
        firstInstance = type.create();
        secondInstance = type.create();
    });

    afterEach(function () {
        Bifrost.dependencyResolver = {};
    });

    it("should return different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});