describe("when adding single requirement explicitly", function () {
    Bifrost.dependencyResolver.getDependenciesFor = function () {
        return [];
    };

    var type = Bifrost.Type.extend(function () {
    }).requires("something");


    it("should only have one dependency", function () {
        expect(type._dependencies.length).toBe(1);
    });

    it("should add the requirement as a dependency", function () {
        expect(type._dependencies[0]).toBe("something");
    });
});