describe("when converting from empty string", function () {

    var converter = Bifrost.values.NumberTypeConverter.create();

    var result = converter.convertFrom("");

    it("should be a number", function () {
        expect(result.constructor).toBe(Number);
    });

    it("should convert to zero", function () {
        expect(result).toBe(0);
    });
});
