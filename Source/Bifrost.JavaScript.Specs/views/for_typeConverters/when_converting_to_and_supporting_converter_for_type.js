describe("when converting to and supporting converter for type", function () {

    var convertedValue = "42";
    var typeConverter = {
        supportedType: Number,
        convertTo: sinon.stub().returns(convertedValue)
    };
    var typeConverterType = { create: function () { return typeConverter; } }
    var typeConverterBefore = null;
    var converted = null;
    beforeEach(function () {
       
        typeConverterBefore = Bifrost.views.TypeConverter;
        Bifrost.views.TypeConverter = {
            getExtenders: function () {
                return [typeConverterType]
            }
        };

        var typeConverters = Bifrost.views.typeConverters.createWithoutScope();
        converted = typeConverters.convertTo(42);
    });

    afterEach(function () {
        Bifrost.views.TypeConverter = typeConverterBefore;
    });

    it("should return the converted value from the converter", function () {
        expect(converted).toBe(convertedValue);
    });
});