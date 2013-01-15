describe("when getting all with one resolver registered", function () {

    Bifrost.InternalDependencyResolver = function () {
        this.isInternal = true;
    };

    Bifrost.DefaultDependencyResolver = function () {
        this.isDefault = true;
    };

    Bifrost.dependencyResolvers.myResolver = {
        identifier: "Hello"
    };

    var resolvers = Bifrost.dependencyResolvers.getAll();

    it("should not get any functions resolvers", function () {
        var hasFunction = false;

        for (var i = 0; i < resolvers.length; i++) {
            if (typeof resolvers[i] === "function") {
                hasFunction = true;
            }
        }

        expect(hasFunction).toBe(false);
    });

    it("should have the registered resolver at the end", function () {
        expect(resolvers[resolvers.length - 1].identifier).toBe("Hello");
    });

    it("should have the internal resolver at the second place", function () {
        expect(resolvers[0].isInternal).toBe(true);
    });

    it("should have the default resolver at the second place", function () {
        expect(resolvers[1].isDefault).toBe(true);
    });
});