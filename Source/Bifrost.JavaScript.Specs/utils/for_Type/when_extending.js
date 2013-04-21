describe("when extending", function () {
    var typeDefinition = function (something) { };
    var result = null;

    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };
        result = Bifrost.Type.extend(typeDefinition);
    });

    afterEach(function () {
        Bifrost.functionParser = {};
    });

    it("should get the dependencies for the function", function () {
        expect(Bifrost.dependencyResolver.getDependenciesFor.called).toBe(true);
    });

    it("should return the type definition", function () {
        expect(result).toBe(typeDefinition);
    });

    it("should a create function", function () {
        expect(typeof result.create).toBe("function");
    });

    it("should add a type id", function () {
        expect(typeDefinition._typeId).toBeDefined();
    });
});