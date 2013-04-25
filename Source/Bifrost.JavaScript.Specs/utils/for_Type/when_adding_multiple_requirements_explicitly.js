describe("when adding multiple requirements explicitly", function () {
    Bifrost.dependencyResolver.getDependenciesFor = function () {
        return [];
    };

    var type = Bifrost.Type.extend(function () {
    }).requires("something", "somethingElse", "andThird");


    it("should only have one dependency", function () {
        expect(type._dependencies.length).toBe(3);
    });

    it("should add the first requirement as a dependency", function () {
        expect(type._dependencies[0]).toBe("something");
    });

    it("should add the second requirement as a dependency", function () {
        expect(type._dependencies[1]).toBe("somethingElse");
    });

    it("should add the first requirement as a dependency", function () {
        expect(type._dependencies[2]).toBe("andThird");
    });
});