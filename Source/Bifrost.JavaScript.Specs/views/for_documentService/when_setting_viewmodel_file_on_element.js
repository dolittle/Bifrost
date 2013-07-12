describe("when setting viewmodel file on element", function () {
    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var element = $("<div/>");
    service.setViewModelFileOn(element, "somefile");
    it("should set it on the data element", function () {
        expect($(element).data("viewmodel-file")).toBe("somefile");
    });

    it("should set the data attribute", function () {
        expect($(element).attr("data-viewmodel-file")).toBe("somefile");
    });
});