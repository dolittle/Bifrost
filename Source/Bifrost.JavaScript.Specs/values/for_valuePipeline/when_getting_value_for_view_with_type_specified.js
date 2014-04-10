describe("when getting value for view with type specified", function () {
    var typeConverters = {
        convertTo: sinon.stub().returns("5")
    };

    var pipeline = Bifrost.values.valuePipeline.createWithoutScope({
        typeConverters: typeConverters
    });

    var element = {};
    var value = ko.observable(5);
    value._typeAsString = "SometType";

    var result = pipeline.getValueForView(element, value);

    it("should try to convert it", function () {
        expect(typeConverters.convertTo.calledWith(5)).toBe(true);
    });

    it("should return the converted value", function () {
        expect(result).toBe("5");
    });
});
