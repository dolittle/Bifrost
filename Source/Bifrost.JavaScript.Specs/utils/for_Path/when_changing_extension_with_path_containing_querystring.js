describe("when changing extension with path containing querystring", function () {

    var newFile = Bifrost.Path.changeExtension("Something/cool/file.html?someParameter=42", "js");

    it("should change the extension", function () {
        expect(newFile).toBe("Something/cool/file.js");
    });
});