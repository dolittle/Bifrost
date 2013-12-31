describe("when setting viewmodel file on element", function () {
    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var viewModel = { something: 42 };
    var element = $("<div/>");
    service.setViewModelOn(element, viewModel);
    it("should set it on the data element", function () {
        expect($(element).data("viewmodel")).toBe(viewModel);
    });

    it("should set it directly on the element", function () {
        expect(element.viewModel).toBe(viewModel);
    });
});