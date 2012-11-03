describe("when namespace only has file matching system needed to be resolved registered in it", function() {
    var resolver = new Bifrost.DefaultDependencyResolver();
    var ns;
    var resolved = null;
    var requireStub;
    var requireArg;
    var canResolve;

    beforeEach(function() {
        ns = {
            _scripts : ["something"]
        };
        requireStub = sinon.stub(window,"require", function(arg) {
            requireArg = arg;
            ns.something = "Hello";
        });
        canResolve = resolver.canResolve(ns, "something");
        resolved = resolver.resolve(ns, "something");
    });

    afterEach(function() {
        requireStub.restore();
    });

    it("should be able to resolve", function () {
        expect(canResolve).toBe(true);
    });

    it("should resolve through require", function () {
        expect(requireArg).toBe("something");
    });

    it("should resolve system loaded into namespace", function() {
        expect(resolved).toBe("Hello");
    });
});