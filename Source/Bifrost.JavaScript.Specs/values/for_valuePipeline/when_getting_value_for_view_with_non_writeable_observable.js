describe("when getting value for view with non writeable observable", function () {
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
    var value = ko.computed(function () { return 42; });

    var result = pipeline.getValueForView(element, value);

    it("should not try to convert it", function () {
        expect(typeConverters.convertTo.called).toBe(false);
    });

    it("should format the value", function () {
        expect(stringFormatter.format.called).toBe(true);
    });

    it("should return the formatted value", function () {
        expect(result).toBe(formattedValue);
    });
});
