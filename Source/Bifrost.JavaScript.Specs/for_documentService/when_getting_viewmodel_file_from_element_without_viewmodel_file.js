describe("when getting viewmodel file from element with viewmodel file", function () {
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var element = $("<div/>");

    var file = service.getViewModelFileFrom(element);
    it("should return an empty string", function () {
        expect(file).toBe("");
    });
});