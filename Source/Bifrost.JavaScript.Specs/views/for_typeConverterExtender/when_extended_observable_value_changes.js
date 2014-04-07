describe("when extended observable value changes", function () {

    var convertedValue = 42;
    var typeConverters = {
        convertFrom: sinon.stub().returns(convertedValue)
    }

    var extender = Bifrost.views.typeConverterExtender.createWithoutScope({
        typeConverters: typeConverters
    });

    var observable = ko.observable();
    var typeAsString = "type";
    extender.extend(observable, typeAsString);

    var newValue = "42";
    observable(newValue);

    it("should forward to typeConverters", function () {
        expect(typeConverters.convertFrom.calledWith(newValue, typeAsString)).toBe(true);
    });

    it("should set the converted value", function () {
        expect(observable()).toBe(convertedValue);
    });
});
