describe("when creating instance with on created function specified also for super", function () {
    var type = null;
    var _super = null;
    var _superOnCreatedCallCount = 0;
    var onCreatedCallCount = 0;

    var instance = null;
    beforeEach(function () {
        _superOnCreatedCallCount = 0;
        onCreatedCallCount = 0;

        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        _super = Bifrost.Type.extend(function () {
            this.onCreated = function () {
                _superOnCreatedCallCount++;
            };
        });

        type = _super.extend(function () {
            this.onCreated = function () {
                onCreatedCallCount++;
            };
        });

        instance = type.create();
    });

    afterEach(function () {
        Bifrost.dependencyResolver = {};
    });

    it("should call the on created function once for the super", function () {
        expect(_superOnCreatedCallCount).toBe(1);
    });

    it("should call the on created function once for the type", function () {
        expect(onCreatedCallCount).toBe(1);
    });
});