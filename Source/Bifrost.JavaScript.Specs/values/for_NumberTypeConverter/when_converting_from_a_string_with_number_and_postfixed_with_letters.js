describe("when converting from a string with number and postfixed with letters", function () {

    var converter = Bifrost.values.NumberTypeConverter.create();

    var result = converter.convertFrom("42something");

    it("should be a number", function () {
        expect(result.constructor).toBe(Number);
    });

    it("should convert to correct number", function () {
        expect(result).toBe(42);
    });
});
