describe("when getting region from element with parent holding the region", function () {
    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var region = { something: 42 };

    var parent = $("<div/>");
    var element = $("<div/>")[0];

    parent.append(element);

    parent[0].region = region;
    var regionFromElement = service.getRegionFor(element);

    it("should get it from the element", function () {
        expect(regionFromElement).toBe(region);
    });
});