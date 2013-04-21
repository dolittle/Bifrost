describe("when creating instance with dependencies and super having dependencies and they are specified", function () {
    var _super = null;
    var type = null;
    var ex = undefined;

    var somethingDependency = {
        something: "hello"
    };
    var somethingElseDependency = {
        somethingElse: "world"
    };

    var superFunction = function (something) {
        this.something = something;
    };

    var typeFunction = function (somethingElse) {
        this.somethingElse = somethingElse;
    };

    var instance = null;

    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: function (func) {
                if (func == superFunction) {
                    return ["something"];
                }
                if (func == typeFunction) {
                    return ["somethingElse"];
                }
            },
        };

        var namespace = { name: "Somewhere" };
        _super = Bifrost.Type.extend(superFunction);
        _super._namespace = namespace;
        type = _super.extend(typeFunction);
        type._namespace = namespace;

        try {
            instance = type.create({
                something: "Hello",
                somethingElse: "World"
            });
        } catch (e) {
            ex = e;
        }
    });


    afterEach(function () {

    });

    it("should not throw an exception", function () {
        expect(ex === undefined).toBe(true);
    });

    it("should create an instance", function () {
        expect(instance).not.toBeNull();
    });

    it("should have the super dependency passed along", function () {
        expect(instance.something).toBe("Hello");
    });

    it("should have the type dependency passed along", function () {
        expect(instance.somethingElse).toBe("World");
    });
});