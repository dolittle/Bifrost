describe("when getting all elements with viewmodel files with three elements only two with viewmodel file", function () {
    var firstElement = $("<div data-viewmodel-file='first'/>")[0];
    var secondElement = $("<div/>")[0];
    var thirdElement = $("<div data-viewmodel-file='second'/>")[0];
    var root = ($("<div/>")
                    .append(firstElement)
                    .append(secondElement)
                    .append(thirdElement)
                )[0];

    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: root
    });

    var elements = service.getAllElementsWithViewModelFiles();

    it("should have two elements", function () {
        expect(elements.length).toBe(2);
    });

    it("should have the first view model element", function () {
        expect(elements[0]).toBe(firstElement);
    });
    it("should have the second view model element", function () {
        expect(elements[1]).toBe(thirdElement);
    });
});