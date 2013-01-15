describe("when resolving a type that is registered", function () {
    Bifrost.WellKnownTypesDependencyResolver.types.something = "Hello";

    var resolver = new Bifrost.WellKnownTypesDependencyResolver();

    var result = resolver.resolve(null, "something");

    it("should return the system it resolves to", function () {
        expect(result).toBe("Hello");
    });
});