describe("when asking for extenders in root namespace", function () {
    var initialType = function (anInitialType) { };
    var namespace = null;
    var extenders = null;

    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };
        initialType = Bifrost.Type.extend(initialType);
        Bifrost.namespace("Root", { RootExtendedType: initialType.extend(function () { }) });
        Bifrost.namespace("Root.Sub", { SubExtendedType: initialType.extend(function (foo) { }) });
        Bifrost.namespace("Root.AnotherSub", { ASecondExtendedType: initialType.extend(function (bar) { }) });

        namespace = Root.RootExtendedType._namespace;

        (function because_of() { extenders = initialType.getExtendersIn(namespace) })();;
    });

    afterEach(function () {
        Bifrost.functionParser = {};
        extenders = null;
    });
    
    it("should return extender on the root level", function () {
        expect(extenders).toContain(Root.RootExtendedType);
    });

    it("should only return a single extender", function () {
        expect(extenders.length).toBe(1);
    });
});
