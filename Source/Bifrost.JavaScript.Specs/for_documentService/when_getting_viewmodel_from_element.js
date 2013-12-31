describe("when setting viewmodel file on element", function () {
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var viewModel = { something: 42 };
    var element = $("<div/>");

    element.viewModel = viewModel;
    var viewModelFromElement = service.getViewModelFrom(element);

    it("should get it from the element", function () {
        expect(viewModelFromElement).toBe(viewModel);
    });
});