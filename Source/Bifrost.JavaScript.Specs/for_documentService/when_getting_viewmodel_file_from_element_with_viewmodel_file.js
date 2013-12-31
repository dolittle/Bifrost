describe("when getting viewmodel file from element with viewmodel file", function () {
    var service = Bifrost.views.documentService.createWithoutScope({
        DOMRoot: {}
    });

    var element = $("<div/>").data("viewmodel-file","somefile")[0];

    var file = service.getViewModelFileFrom(element);
    it("should return the filename", function () {
        expect(file).toBe("somefile");
    });
});