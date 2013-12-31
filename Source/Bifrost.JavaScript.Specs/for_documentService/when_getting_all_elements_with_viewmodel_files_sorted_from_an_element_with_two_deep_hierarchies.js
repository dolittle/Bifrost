describe("when getting all elements with viewmodel files sorted from an element with two deep hierarchies", function () {
    var rootInFirstHiearchyElement = $("<div data-viewmodel-file='firstHierarchy'/>")[0];
    var firstLevelInFirstHiearchyElement = $("<div data-viewmodel-file='firstHierarchyFirstLevel'/>")[0];
    var secondLevelInFirstHiearchyElement = $("<div data-viewmodel-file='firstHierarchySecondLevel'/>")[0];

    rootInFirstHiearchyElement.appendChild(firstLevelInFirstHiearchyElement);
    firstLevelInFirstHiearchyElement.appendChild(secondLevelInFirstHiearchyElement);

    var rootInSecondHiearchyElement = $("<div data-viewmodel-file='secondHierarchy'/>")[0];
    var firstLevelInSecondHiearchyElement = $("<div data-viewmodel-file='secondHierarchyFirstLevel'/>")[0];
    var secondLevelInSecondHiearchyElement = $("<div data-viewmodel-file='secondHierarchySecondLevel'/>")[0];

    rootInSecondHiearchyElement.appendChild(firstLevelInSecondHiearchyElement);
    firstLevelInSecondHiearchyElement.appendChild(secondLevelInSecondHiearchyElement);

    var root = ($("<div/>")
                    .append(rootInFirstHiearchyElement)
                    .append(rootInSecondHiearchyElement)
                )[0];

    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var elements = service.getAllElementsWithViewModelFilesSortedFrom(root);

    it("should have six elements", function () {
        expect(elements.length).toBe(6);
    });

    it("should have the first hierarchy's second level at position 0", function () {
        expect(elements[0]).toBe(secondLevelInFirstHiearchyElement);
    });

    it("should have the first hierarchy's first level at position 1", function () {
        expect(elements[1]).toBe(firstLevelInFirstHiearchyElement);
    });

    it("should have the first hierarchy's root level at position 2", function () {
        expect(elements[2]).toBe(rootInFirstHiearchyElement);
    });

    it("should have the second hierarchy's second level at position 3", function () {
        expect(elements[3]).toBe(secondLevelInSecondHiearchyElement);
    });

    it("should have the second hierarchy's first level at position 4", function () {
        expect(elements[4]).toBe(firstLevelInSecondHiearchyElement);
    });

    it("should have the second hierarchy's root level at position 5", function () {
        expect(elements[5]).toBe(rootInSecondHiearchyElement);
    });
});