describe("when changing extension with path path prefixed with dots and no extension on original", function () {

    var newFile = Bifrost.Path.changeExtension("../Something/cool/file", "js");

    it("should change the extension", function () {
        expect(newFile).toBe("../Something/cool/file.js");
    });
});