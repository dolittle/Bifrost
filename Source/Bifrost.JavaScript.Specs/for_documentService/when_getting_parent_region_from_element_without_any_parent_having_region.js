describe("when getting parent region from element with parent holding the region", function () {
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var parent = $("<div/>");
    var element = $("<div/>")[0];
    parent.append(element);

    var regionFromElement = service.getParentRegionFor(element);

    it("should return null", function () {
        expect(regionFromElement).toBeNull();
    });
});