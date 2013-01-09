describe("when creating instance", function () {

    var type = null;

    var instance = null;
    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = Bifrost.Type.extend(function () {
        });

        instance = type.create();
    });

    afterEach(function () {
        Bifrost.dependencyResolver = {};
    });

    it("should return an instance", function () {
        expect(instance).not.toBeNull();
    });

    it("should have type info on it", function () {
        expect(instance._type).not.toBeUndefined();
    });

    it("should have correct type in type info", function () {
        expect(instance._type._name).toBe("Type");
    });
});