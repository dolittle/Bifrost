describe("when getting region from element and there is no region to find", function () {
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var element = $("<div/>")[0];
    var regionFromElement = service.getRegionFor(element);

    it("should return null", function () {
        expect(regionFromElement).toBeNull();
    });
});