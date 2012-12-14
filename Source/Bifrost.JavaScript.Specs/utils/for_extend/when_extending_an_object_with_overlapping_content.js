
describe("when extending an object with overlapping content", function () {
    var overlappingObject = {
        property: "horse",
        func: "",
        anotherProperty: []
    },
    extension = {
        property: "sheep",
        func: function () { }
    };
    Bifrost.extend(overlappingObject, extension);
    it("should add property", function () {
        expect(overlappingObject.property).toEqual(extension.property);
    });
    it("should add function", function () {
        expect(overlappingObject.func).toEqual(extension.func);
    });
    it("should still contain the base property", function () {
        expect(overlappingObject.anotherProperty).toEqual([]);
    });
});