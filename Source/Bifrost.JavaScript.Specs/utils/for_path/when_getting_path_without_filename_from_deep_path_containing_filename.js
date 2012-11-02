describe("when getting path without filename from deep path containing filename", function () {
    var path = "something/cool/file.js";
    var result = Bifrost.path.getPathWithoutFilename(path);

    it("should get only the path", function () {
        expect(result).toBe("something/cool");
    });
});