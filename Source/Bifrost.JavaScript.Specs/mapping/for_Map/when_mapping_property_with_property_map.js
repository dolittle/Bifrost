describe("when mapping property with property map", function () {
    var propertyMapType = null;
    var result = null;
    var source = { SomeProperty: 42 };
    var target = {};

    var propertyMap = {
        map: sinon.stub()
    };

    beforeEach(function () {
        propertyMapType = Bifrost.mapping.PropertyMap;
        Bifrost.mapping.PropertyMap = {
            create: sinon.stub().returns(propertyMap)
        };

        var map = Bifrost.mapping.Map.create();
        map.property("SomeProperty");

        map.mapProperty("SomeProperty", source, target);
    });

    afterEach(function () {
        Bifrost.mapping.PropertyMap = propertyMapType;
    });

    it("should forward mapping to the property map", function () {
        expect(propertyMap.map.calledWith(source, target)).toBe(true);
    });
});