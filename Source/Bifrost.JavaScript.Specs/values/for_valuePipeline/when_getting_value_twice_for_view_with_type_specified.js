describe("when getting value twice for view with string format", function () {
    var typeConverters = {
        convertTo: sinon.stub().returns("5")
    };
    var stringFormatter = {
        hasFormat: sinon.stub().returns(false)
    };

    var pipeline = Bifrost.values.valuePipeline.createWithoutScope({
        typeConverters: typeConverters,
        stringFormatter: stringFormatter
    });

    var element = {};
    var value = ko.observable(5);
    value._typeAsString = "SometType";

    var firstResult = pipeline.getValueForView(element, value);
    var secondResult = pipeline.getValueForView(element, value);

    it("should try to convert it", function () {
        expect(typeConverters.convertTo.calledWith(5)).toBe(true);
    });

    it("should return same observable", function () {
        expect(firstResult).toBe(secondResult);
    });

    it("should return the converted value for the first result", function () {
        expect(firstResult).toBe("5");
    });

    it("should return the converted value for the second result", function () {
        expect(secondResult).toBe("5");
    });
});
