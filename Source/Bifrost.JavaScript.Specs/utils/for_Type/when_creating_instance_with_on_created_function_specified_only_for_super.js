describe("when creating instance with on created function specified only for super", function () {
    var type = null;
    var _super = null;
    var _superOnCreatedCallCount = 0;
    var descendant = null;

    var instance = null;
    beforeEach(function () {
        _superOnCreatedCallCount = 0;
        onCreatedCallCount = 0;

        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        _super = Bifrost.Type.extend(function () {
            this.someValue = "Hello";

            this.onCreated = function (lastDescendant) {
                _superOnCreatedCallCount++;
                descendant = lastDescendant;
            };
        });

        type = _super.extend(function () {
        });

        instance = type.create();
    });

    afterEach(function () {
        Bifrost.dependencyResolver = {};
    });

    it("should call the on created function once for the super", function () {
        expect(_superOnCreatedCallCount).toBe(1);
    });

    it("should pass the correct descendant in as parameter", function () {
        expect(descendant).toBe(instance);
    });
});