describe("when converting from a number", function () {
    var converter = Bifrost.values.StringTypeConverter.create();

    var result = converter.convertFrom(42.43);

    it("should convert to correct string", function () {
        expect(result).toBe("42.43");
    });
});