describe("when getting all elements from an element with viewmodel files with one element with viewmodel file", function () {
    var viewModelElement = $("<div data-viewmodel-file='something'/>")[0];
    var root = ($("<div/>").append(viewModelElement))[0];
    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var elements = service.getAllElementsWithViewModelFilesFrom(root);
    it("should have one element", function () {
        expect(elements.length).toBe(1);
    });

    it("should have the view model element", function () {
        expect(elements[0]).toBe(viewModelElement);
    });
});