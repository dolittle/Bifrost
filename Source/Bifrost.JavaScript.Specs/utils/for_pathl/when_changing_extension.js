describe("when changing extension", function () {

    var newFile = Bifrost.Path.changeExtension("Something/cool/file.html", "js");

    it("should change the extension", function () {
        expect(newFile).toBe("Something/cool/file.js");
    });
});