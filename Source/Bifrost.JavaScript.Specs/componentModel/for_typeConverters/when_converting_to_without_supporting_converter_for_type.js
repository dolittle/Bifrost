describe("when converting to without supporting converter for type", function () {

    var convertedValue = 42;
    var typeConverter = {
        supportedType: Date,
        convertTo: sinon.stub().returns(convertedValue)
    };
    var typeConverterType = { create: function () { return typeConverter; } }
    var typeConverterBefore = null;
    var converted = null;
    var input = "42";
    beforeEach(function () {
       
        typeConverterBefore = Bifrost.componentModel.TypeConverter;
        Bifrost.componentModel.TypeConverter = {
            getExtenders: function () {
                return [typeConverterType]
            }
        };

        var typeConverters = Bifrost.componentModel.typeConverters.createWithoutScope();
        converted = typeConverters.convertTo(input, Number);
    });

    afterEach(function () {
        Bifrost.views.TypeConverter = typeConverterBefore;
    });

    it("should return the same value as input", function () {
        expect(converted).toBe(input);
    });
});