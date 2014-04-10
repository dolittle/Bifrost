describe("when converting from a float string with dot as separator", function () {

    var converter = Bifrost.values.NumberTypeConverter.create();

    var result = converter.convertFrom("42.43");

    it("should be a number", function () {
        expect(result.constructor).toBe(Number);
    });

    it("should convert to correct number", function () {
        expect(result).toBe(42.43);
    });
});
