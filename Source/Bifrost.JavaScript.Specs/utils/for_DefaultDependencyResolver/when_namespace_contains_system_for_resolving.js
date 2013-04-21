describe("when namespace contains system for resolving", function () {
    var canResolve = false;
    var resolved = null;


    beforeEach(function () {
        var resolver = new Bifrost.DefaultDependencyResolver();
        var ns = {
            something: "Hello"
        }
        canResolve = resolver.canResolve(ns, "something");
        resolved = resolver.resolve(ns, "something");
    });

    it("should be able to resolve", function () {
        expect(canResolve).toBe(true);
    });

    it("should resolve system", function () {
        expect(resolved).toBe("Hello");
    });
});