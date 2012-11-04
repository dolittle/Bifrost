describe("when namespace only has file matching system needed to be resolved registered in it", function() {
    var resolver = new Bifrost.DefaultDependencyResolver();
    var ns;
    var resolved = null;
    var actualResolved = null;
    var requireStub;
    var requireArg;
    var canResolve;

    beforeEach(function() {
        ns = {
            _scripts : ["something"]
        };
        requireStub = sinon.stub(window,"require", function(arg, callback) {
            requireArg = arg[0];
            ns.something = "Hello";

            callback();
        });
        canResolve = resolver.canResolve(ns, "something");
        resolved = resolver.resolve(ns, "something");
        resolved.continueWith(function(nextPromise, arg) {
            actualResolved = arg;
        });
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

    it("should return a promise", function() {
        expect(resolved.constructor).toBe(Bifrost.execution.Promise);
    });

    it("should resolve system loaded into namespace", function() {
        expect(actualResolved).toBe("Hello");
    });
});