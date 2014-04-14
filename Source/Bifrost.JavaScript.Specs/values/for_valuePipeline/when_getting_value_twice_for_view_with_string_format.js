describe("when getting value twice for view with type specified", function () {
    var typeConverters = {
        convertTo: sinon.stub()
    };
    var formattedValue = "5 -5";

    var stringFormatter = {
        hasFormat: sinon.stub().returns(true),
        format: sinon.stub().returns(formattedValue)
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

    it("should not try to convert it", function () {
        expect(typeConverters.convertTo.called).toBe(false);
    });

    it("should format the value", function () {
        expect(stringFormatter.format.calledWith(element, 5)).toBe(true);
    });

    it("should return same observable", function () {
        expect(firstResult).toBe(secondResult);
    });

    it("should return the formatted value for first result", function () {
        expect(firstResult).toBe(formattedValue);
    });

    it("should return the formatted value for second result", function () {
        expect(secondResult).toBe(formattedValue);
    });
});
