describe("when getting all with one resolver registered", function () {

    var wellKnownTypesDependencyResolver = Bifrost.WellKnownTypesDependencyResolver;
    var defaultDependencyResolver = Bifrost.DefaultDependencyResolver;
    var knownArtifactTypesDependencyResolver = Bifrost.KnownArtifactTypesDependencyResolver;
    var knownArtifactInstancesDependencyResolver = Bifrost.KnownArtifactInstancesDependencyResolver;

    Bifrost.WellKnownTypesDependencyResolver = function () {
        this.isWellKnown = true;
    };

    Bifrost.DefaultDependencyResolver = function () {
        this.isDefault = true;
    };

    Bifrost.KnownArtifactTypesDependencyResolver = function () { };
    Bifrost.KnownArtifactInstancesDependencyResolver = function () { };

    Bifrost.dependencyResolvers.myResolver = {
        identifier: "Hello"
    };

    var resolvers = Bifrost.dependencyResolvers.getAll();

    Bifrost.WellKnownTypesDependencyResolver = wellKnownTypesDependencyResolver;
    Bifrost.DefaultDependencyResolver = defaultDependencyResolver;
    Bifrost.KnownArtifactTypesDependencyResolver = knownArtifactTypesDependencyResolver;
    Bifrost.KnownArtifactInstancesDependencyResolver = knownArtifactInstancesDependencyResolver;

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

    it("should have the well known type resolver at the second place", function () {
        expect(resolvers[0].isWellKnown).toBe(true);
    });

    it("should have the default resolver at the second place", function () {
        expect(resolvers[1].isDefault).toBe(true);
    });
});