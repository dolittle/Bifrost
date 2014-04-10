describe("when converting from an integer string", function () {

    var converter = Bifrost.componentModel.NumberTypeConverter.create();

    var result = converter.convertFrom("42");

    it("should be a number", function () {
        expect(result.constructor).toBe(Number);
    });

    it("should convert to correct number", function () {
        expect(result).toBe(42);
    });
});
