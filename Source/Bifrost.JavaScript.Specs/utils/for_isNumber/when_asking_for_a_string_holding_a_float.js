describe("when asking for a string holding a float", function () {

    var result = Bifrost.isNumber("1.1");

    it("should return true", function () {
        expect(result).toBe(true);
    });
});