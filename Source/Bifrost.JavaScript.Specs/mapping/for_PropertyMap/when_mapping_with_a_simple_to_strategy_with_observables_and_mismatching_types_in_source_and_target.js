describe("when mapping with a simple to strategy with observables and mismatching types in source and target", function () {
    var propertyMap = Bifrost.mapping.PropertyMap.create({
        sourceProperty: "sourceProperty",
        typeConverters: {
            convertFrom: sinon.stub().returns(42)
        }
    });

    var source = {
        sourceProperty: ko.observable("42")
    };
    var target = {
        targetProperty: ko.observable(43)

    };
    
    propertyMap.to("targetProperty");

    propertyMap.map(source, target);

    it("should copy the source property value to the target property", function () {
        expect(target.targetProperty()).toBe(42);
    });
});