describe("when asking if element has own region and it does not", function () {
    var service = Bifrost.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var element = $("<div/>")[0];
    element.region = { something: 42 };
    var hasRegion = service.hasOwnRegion(element);

    it("should return that it has", function () {
        expect(hasRegion).toBe(true);
    });
});