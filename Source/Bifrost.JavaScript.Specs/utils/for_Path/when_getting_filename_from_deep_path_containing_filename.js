describe("when getting path without filename from deep path containing filename", function () {
    var path = "something/cool/file.js";
    var result = Bifrost.Path.getFilename(path);

    it("should get only the file", function () {
        expect(result).toBe("file.js");
    });
});