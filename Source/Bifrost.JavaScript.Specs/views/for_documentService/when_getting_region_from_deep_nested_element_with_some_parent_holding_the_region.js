describe("when getting region from deep nested element with some parent holding the region", function () {
    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var region = { something: 42 };

    var body = $("<body/>");
    var parent = $("<div/>");
    var element = $("<div/>")[0];

    parent.append(element);
    body.append(element);

    body[0].region = region;
    var regionFromElement = service.getRegionFrom(element);

    it("should get it from the element", function () {
        expect(regionFromElement).toBe(region);
    });
});