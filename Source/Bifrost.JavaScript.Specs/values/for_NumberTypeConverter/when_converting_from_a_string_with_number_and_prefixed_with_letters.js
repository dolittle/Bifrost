describe("when converting from a string with number and prefixed with letters", function () {

    var converter = Bifrost.values.NumberTypeConverter.create();

    var result = converter.convertFrom("something42");

    it("should be a number", function () {
        expect(result.constructor).toBe(Number);
    });

    it("should convert to correct number", function () {
        expect(result).toBe(42);
    });
});
