describe("when getting value for property without type specified", function () {
    var typeConverters = {
        convertFrom: sinon.stub()

    };

    var pipeline = Bifrost.values.valuePipeline.createWithoutScope({
        typeConverters: typeConverters
    });

    var value = ko.observable(42);

    var result = pipeline.getValueForProperty(value, value());
    
    it("should not try to convert type", function () {
        expect(typeConverters.convertFrom.called).toBe(false);
    });

    it("should return same as input", function () {
        expect(result).toBe(value());
    });
});
