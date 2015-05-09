describe("when converting from a default date string", function () {

    var converter = Bifrost.values.DateTypeConverter.create();
    var result1 = converter.convertFrom("0001-01-01T00:00:00");
    var result2 = converter.convertFrom("0001-01-01T00:00:00Z");
    var result3 = converter.convertFrom(null);
    var result4 = converter.convertFrom(new Date("0001-01-01T00:00:00"));
    var result5 = converter.convertFrom(new Date("0001-01-01T00:00:00Z"));

    it("should return a null result in case 1", function () {
        expect(result1).toBeNull();
    });

    it("should return a null result in case 2", function () {
        expect(result2).toBeNull();
    });

    it("should return a null result in case 3", function () {
        expect(result3).toBeNull();
    });

    it("should return a null result in case 4", function () {
        expect(result4).toBeNull();
    });

    it("should return a null result in case 5", function () {
        expect(result5).toBeNull();
    });
});

