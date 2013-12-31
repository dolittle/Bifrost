describe("when getting all elements with viewmodel files with one element with viewmodel file", function () {
    var viewModelElement = $("<div data-viewmodel-file='something'/>")[0];
    var root = ($("<div/>").append(viewModelElement))[0];
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: root
    });

    var elements = service.getAllElementsWithViewModelFiles();

    it("should have one element", function () {
        expect(elements.length).toBe(1);
    });

    it("should have the view model element", function () {
        expect(elements[0]).toBe(viewModelElement);
    });
});