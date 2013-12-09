describe("when getting path without filename without extension from deep path containing filename", function () {
    var path = "something/cool/file.js";
    var result = Bifrost.Path.getFilenameWithoutExtension(path);

    it("should get only the file", function () {
        expect(result).toBe("file");
    });
});