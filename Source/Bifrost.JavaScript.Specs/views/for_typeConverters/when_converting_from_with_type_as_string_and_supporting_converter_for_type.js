describe("when converting from with type as string and supporting converter for type", function () {

    var convertedValue = 42;
    var typeConverter = {
        supportedType: Number,
        convertFrom: sinon.stub().returns(convertedValue)
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
        converted = typeConverters.convertFrom("42", "Number");
    });

    afterEach(function () {
        Bifrost.views.TypeConverter = typeConverterBefore;
    });

    it("should return the converted value from the converter", function () {
        expect(converted).toBe(convertedValue);
    });
});