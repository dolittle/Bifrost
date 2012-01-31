describe("when extending an empty object", function () {
    var emptyObject = {},
    extension = {
        property: "sau",
        func: function () { }
    };
    Bifrost.extend(emptyObject, extension);
    it("should add property", function () {
        expect(emptyObject.property).toEqual(extension.property);
    });
    it("should add function", function () {
        expect(emptyObject.func).toEqual(extension.func);
    });
});

