describe("when getting for an element that has region", function () {
    var region = {
        existing: "region"
    };

    var element = {
        DOM:"element"
    };

    var view = {
        element: element
    };

    var documentService = {
        hasOwnRegion: sinon.mock().withArgs(element).returns(true),
        getRegionFor: sinon.mock().withArgs(element).returns(region)
    };

    var instance = Bifrost.views.regionManager.createWithoutScope({
        documentService: documentService
    });

    var regionReturned = null;
    instance.getFor(view).continueWith(function(instance) {
        regionReturned = instance;
    });

    it("should get the region from the element", function () {
        expect(regionReturned).toBe(region);
    });
});