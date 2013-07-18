describe("when creating instance of a scoped type explicitly ignoring the scope", function () {
    var existingGetDependenciesFor = null;
    var firstInstance = 41;
    var secondInstance = 42;

    beforeEach(function () {
        existingGetDependenciesFor = Bifrost.dependencyResolver.getDependenciesFor;
        Bifrost.dependencyResolver.getDependenciesFor = function () { return []; };

        var counter = 0;
        var type = Bifrost.Type.extend(function () {
            var self = this;

            this.id = counter++;
        }).scopeTo(window);

        firstInstance = type.createWithoutScope();
        secondInstance = type.createWithoutScope();
    });

    afterEach(function () {
        Bifrost.dependencyResolver.getDependenciesFor = existingGetDependenciesFor;
    });

    it("should have two different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});