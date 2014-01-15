describe("when asking if url is absolute and it is relative", function () {

    var result = Bifrost.Uri.isAbsolute("example/url");

    it("should be considered relative", function () {
        expect(result).toBe(false);
    });


});