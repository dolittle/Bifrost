describe("when mapping without strategy set", function () {
    var propertyMap = Bifrost.mapping.PropertyMap.create({
        sourceProperty: "Source",
        typeConverters: {}
    });

    var missingPropertyStrategy;
    var exception = null;

    beforeEach(function () {
        missingPropertyStrategy = Bifrost.mapping.MissingPropertyStrategy;
        Bifrost.mapping.MissingPropertyStrategy = Bifrost.Type.extend(function () { });

        
        try {
            propertyMap.map({}, {});
        } catch (e) {

            exception = e;
        }
    });

    afterEach(function () {
        Bifrost.mapping.MissingPropertyStrategy = missingPropertyStrategy;
    });

    it("should throw missing property strategy", function () {
        expect(exception._type).toBe(Bifrost.mapping.MissingPropertyStrategy);
    });
});