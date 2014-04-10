describe("when extended observable value changes", function () {
    var extender = Bifrost.values.typeConverterExtender.createWithoutScope();

    var observable = ko.observable();
    var typeAsString = "type";
    extender.extend(observable, typeAsString);

    it("observable should have type information", function () {
        expect(observable._typeAsString).toBe("type");
    });
});
