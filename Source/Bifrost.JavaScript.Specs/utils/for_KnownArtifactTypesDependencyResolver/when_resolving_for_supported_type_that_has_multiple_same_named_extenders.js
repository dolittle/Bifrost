describe("when resolving for supported type that has multiple same named extenders", sinon.test(function () {

    var resolver,
        resolvedTypes,
        propertyToResolve,
        namespace,
        getExtendersIn = function (namespace) { return [Test.extender, Test.Deeper.extender, Test.Deeper.EvenDeeper.extender]; };

    beforeEach(function () {
        Bifrost.namespace("Test", { extender: Bifrost.Type.extend(function () { }) });
        Bifrost.namespace("Test.Deeper", { extender: Bifrost.Type.extend(function () { }) });
        Bifrost.namespace("Test.Deeper.EvenDeeper", { extender: Bifrost.Type.extend(function () { }) });
        namespace = Test.Deeper.EvenDeeper.extender._namespace;

        Bifrost.commands.Command = { getExtendersIn: getExtendersIn };

        resolver = new Bifrost.KnownArtifactTypesDependencyResolver();
        propertyToResolve = "commandTypes";
        namespace = {};
        
        resolvedTypes = resolver.resolve(namespace, propertyToResolve);
    });

    it("should resolve types", function () {
        expect(resolvedTypes).toBeDefined();
    })

    it("should have the most specific type", function () {
        expect(resolvedTypes.extender).toBe(Test.Deeper.EvenDeeper.extender);
    })

}));