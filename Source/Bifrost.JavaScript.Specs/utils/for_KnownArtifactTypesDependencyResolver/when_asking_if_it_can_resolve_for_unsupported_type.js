describe("when asking if it can resolve for unsupported", sinon.test(function () {

    var resolver,
        canResolve,
        propertyToResolve,
        namespace;

    beforeEach(function () {
        Bifrost.commands = sinon.stub().returns({ Command: function () { } });
        Bifrost.read = sinon.stub().returns({
            ReadModelOf: function () { },
            Query: function () { }
        });

        resolver = new Bifrost.KnownArtifactTypesDependencyResolver();
        canResolve = false;
        propertyToResolve = "someOtherType";
        namespace = {};
        
        canResolve = resolver.canResolve(namespace, propertyToResolve);
    });

    it("should return false", function () {
        expect(canResolve).toBe(false);
    })

}));