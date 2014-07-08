describe("when creating from instance with no properties", function () {
    var instance = {};

    var typeInfo = Bifrost.types.TypeInfo.createFrom(instance);

    it("should hold no properties", function () {
        expect(typeInfo.properties.length).toBe(0);
    });
})