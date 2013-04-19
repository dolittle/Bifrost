describe("when making path starting with slash relative", function () {
    var relative = Bifrost.path.makeRelative("/absolute/path.js");

    it("should removing slash", function () {
        expect(relative).toBe("absolute/path.js");
    });
});