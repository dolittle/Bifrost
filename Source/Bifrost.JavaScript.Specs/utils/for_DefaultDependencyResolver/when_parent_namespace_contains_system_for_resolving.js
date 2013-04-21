describe("when parent namespace contains system for resolving", function () {
    var resolver = new Bifrost.DefaultDependencyResolver();

    Bifrost.namespace("Parent", {
        something: "Hello"
    });
    Bifrost.namespace("Parent.Child", {
    });

    var ns = Parent.Child;

    var canResolve = resolver.canResolve(ns, "something");

    var resolved = resolver.resolve(ns, "something");

    it("should be able to resolve", function () {
        expect(canResolve).toBe(true);
    });

    it("should resolve system", function () {
        expect(resolved).toBe("Hello");
    });
});