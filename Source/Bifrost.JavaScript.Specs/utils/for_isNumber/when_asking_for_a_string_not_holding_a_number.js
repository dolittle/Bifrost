describe("when asking for a string not holding a number", function () {

    var result = Bifrost.isNumber("1b");

    it("should return false", function () {
        expect(result).toBe(false);
    });
});