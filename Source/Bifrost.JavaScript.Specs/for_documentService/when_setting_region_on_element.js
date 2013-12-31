describe("when setting region on element", function () {
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var element = $("<div/>")[0];
    var region = { something: 42 };
    
    var hasRegion = service.setRegionOn(element, region);

    it("should hold region om element", function () {
        expect(element.region).toBe(region);
    });
});