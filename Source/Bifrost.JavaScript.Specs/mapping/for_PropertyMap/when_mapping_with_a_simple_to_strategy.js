describe("when mapping with a simple to strategy", function () {
    var propertyMap = Bifrost.mapping.PropertyMap.create({
        sourceProperty: "sourceProperty"
    });

    var source = {
        sourceProperty: 42
    };
    var target = {};
    
    propertyMap.to("targetProperty");

    propertyMap.map(source, target);

    it("should copy the source property value to the target property", function () {
        expect(target.targetProperty).toBe(42);
    });
});