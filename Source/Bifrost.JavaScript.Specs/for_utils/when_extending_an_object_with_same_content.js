
describe("when extending an object with same content", function () {
    var contentObject = {
        property: "",
        func: function (param) { }
    },
    extension = {
        property: "sheep",
        func: function () { }
    };
    Bifrost.extend(contentObject, extension);
    it("should add property", function () {
        expect(contentObject.property).toEqual(extension.property);
    });
    it("should add function", function () {
        expect(contentObject.func).toEqual(extension.func);
    });
});
