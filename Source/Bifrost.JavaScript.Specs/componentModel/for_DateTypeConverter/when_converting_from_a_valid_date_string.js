describe("when converting from a valid date string", function () {

    var converter = Bifrost.componentModel.DateTypeConverter.create();
    var result = converter.convertFrom("2014/2/27");

    it("should return a result", function () {
        expect(result).toBeDefined();
    });

    it("should return a date instance", function () {
        expect(result.constructor).toBe(Date);
    });

    it("should parse correct year", function () {
        expect(result.getFullYear()).toBe(2014);
    });
    
    it("should parse correct month", function () {
        expect(result.getMonth()).toBe(1);
    });

    it("should parse correct day", function () {
        expect(result.getDate()).toBe(27);
    });
});