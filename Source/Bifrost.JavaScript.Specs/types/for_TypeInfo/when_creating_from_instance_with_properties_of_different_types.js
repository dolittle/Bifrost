describe("when creating from instance with no properties", function () {
    var sometype = Bifrost.Type.extend(function () { });

    var instance = {
        numberProperty: 42,
        stringProperty: "Fourty two",
        booleanProperty: true,
        objectProperty: {},
        sometypeProperty: sometype.create(),
    };

    var typeInfo = Bifrost.types.TypeInfo.createFrom(instance);
    function getPropertyInfoByName(name) {
        var found = null;
        typeInfo.properties.forEach(function (property) {
            if (property.name == name) {
                found = property;
                return;
            }
        });

        return found;
    }

    it("should hold number property of correct type", function () {
        expect(getPropertyInfoByName("numberProperty").type).toBe(Number);
    });

    it("should hold string property of correct type", function () {
        expect(getPropertyInfoByName("stringProperty").type).toBe(String);
    });

    it("should hold boolean property of correct type", function () {
        expect(getPropertyInfoByName("booleanProperty").type).toBe(Boolean);
    });

    it("should hold object property of correct type", function () {
        expect(getPropertyInfoByName("objectProperty").type).toBe(Object);
    });

    it("should hold sometype property of correct type", function () {
        expect(getPropertyInfoByName("sometypeProperty").type).toBe(sometype);
    });
});