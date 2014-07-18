describe("when mapping to instance with matching object property", function(){
	var data = { objectProperty: {objectProperty : "value"}};

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "";
        this.numberProperty = 0;
        this.arrayProperty = [];
        this.objectProperty = {
        	objectProperty : ""
        };
	});

	var mappedInstance = type.create();

	(function becauseOf(){
		var mapper = Bifrost.mapping.mapper.create(parameters);
		mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should map the corresponding objectProperty", function(){
		expect(mappedInstance.objectProperty).toEqual(data.objectProperty);
	});

	it("should map the objects properties value", function() {
		expect(mappedInstance.objectProperty.objectProperty).toEqual("value")
	});

});