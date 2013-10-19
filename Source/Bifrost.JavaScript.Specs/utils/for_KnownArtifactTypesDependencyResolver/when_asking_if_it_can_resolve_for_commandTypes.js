describe("when asking if it can resolve for comamndTypes", sinon.test(function () {

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
        propertyToResolve = "commandTypes";
        namespace = {};
        
        canResolve = resolver.canResolve(namespace, propertyToResolve);
    });

    it("should return true", function () {
        expect(canResolve).toBe(true);
    })

}));