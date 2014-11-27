describe("when mapping to instance with map that has propertymap with different property name for one of the given properties", function () {
    var data = { number: 42, someString: "asdasd" };

    var mapStub = {
        canMapProperty: function (property) {
            return property == "number";
        },
        mapProperty: sinon.stub()
    };

    var parameters = {
        typeConverters: {},
        maps: {
            hasMapFor: sinon.stub().returns(true),
            getMapFor: sinon.stub().returns(mapStub)
        }
    };

    var type = Bifrost.Type.extend(function () {
        this.targetNumber = 0;
    });

    var mappedInstance = type.create();

    var mapper = Bifrost.mapping.mapper.create(parameters);
    var mappedProperties = mapper.mapToInstance(type, data, mappedInstance);

    it("should map the property through the map", function () {
        expect(mapStub.mapProperty.calledWith("number", data, mappedInstance)).toBe(true);
    });

    it("should contain the property can be mapped in mapped properties", function () {
        expect(mappedProperties.indexOf("number") >= 0).toBe(true);
    });

    it("should not contain the property that can not be mapped in mapped properties", function () {
        expect(mappedProperties.indexOf("someString") < 0).toBe(true);
    });
});