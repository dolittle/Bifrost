describe("when asking for a string holding an integer", function () {

    var result = Bifrost.isNumber("1");

    it("should return true", function () {
        expect(result).toBe(true);
    });
});