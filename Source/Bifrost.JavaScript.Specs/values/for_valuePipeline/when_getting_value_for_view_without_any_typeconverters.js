describe("when getting value for view without any typeconverters", function () {
    var typeConverters = {
        convertTo: sinon.stub()
    };
    var stringFormatter = {
        hasFormat: sinon.stub().returns(false)
    };

    var pipeline = Bifrost.values.valuePipeline.createWithoutScope({
        typeConverters: typeConverters,
        stringFormatter: stringFormatter
    });

    var element = {};
    var value = ko.observable();

    var result = pipeline.getValueForView(element, value);

    it("should not try to convert it", function () {
        expect(typeConverters.convertTo.called).toBe(false);
    });

    it("should return the same observable as given", function () {
        expect(result).toBe(value);
    });
});
