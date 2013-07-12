describe("when getting all elements from an element with viewmodel files with root and one child having viewmodel file", function () {
    var firstElement = $("<div />")[0];
    var secondElement = $("<div/>")[0];
    var thirdElement = $("<div data-viewmodel-file='second'/>")[0];
    var root = ($("<div data-viewmodel-file='root'/>")
                    .append(firstElement)
                    .append(secondElement)
                    .append(thirdElement)
                )[0];

    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var elements = service.getAllElementsWithViewModelFilesFrom(root);

    it("should have two elements", function () {
        expect(elements.length).toBe(2);
    });

    it("should have the third view model element", function () {
        expect(elements[0]).toBe(thirdElement);
    });
    it("should have the root view model element", function () {
        expect(elements[1]).toBe(root);
    });
});