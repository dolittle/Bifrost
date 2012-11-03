describe("when namespace only has file matching system needed to be resolved registered in it", function() {
    var resolver = new Bifrost.DefaultDependencyResolver();
    var ns = {
        _scripts : ["something"]
    };
    var canResolve = resolver.canResolve(ns, "something");
    var resolved = null;
    var requireStub;
    var requireArg;

    beforeEach(function() {
        requireStub = sinon.stub(window,"require", function(arg) {
            requireArg = arg;
            print("Require");
            ns.something = "Hello";
        });
        print("Resolve");
        resolved = resolver.resolve(ns, "something");
    });

    afterEach(function() {
        requireStub.restore();
    });

    it("should return true", function () {
        expect(canResolve).toBe(true);
    });

    it("should resolve through require", function () {
        expect(requireArg).toBe("something");
    });

    it("should return system loaded into namespace", function() {
        expect(resolved).toBe("Hello");
    });
});