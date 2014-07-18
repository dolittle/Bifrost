describe("when defining property", function () {
    var propertyMapType = null;
    var propertyMapInstance = { something: 42 };
    var propertyMap = null;
    beforeEach(function () {
        propertyMapType = Bifrost.mapping.PropertyMap;
        Bifrost.mapping.PropertyMap = {
            create: sinon.stub().returns(propertyMapInstance)
        };

        var map = Bifrost.mapping.Map.create();
        propertyMap = map.property("SomeProperty");
    });


    afterEach(function () {
        Bifrost.mapping.PropertyMap = propertyMapType;
    });

    it("should create a new property map", function () {
        expect(Bifrost.mapping.PropertyMap.create.calledWith({ sourceProperty: "SomeProperty"})).toBe(true)
    });

    it("should return the created property map", function () {
        expect(propertyMap).toBe(propertyMapInstance);
    });
    
});