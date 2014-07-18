describe("when mapping to type with matching object property", function () {
	var mappedInstance;
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

	var returnedInstance = type.create();

    type.create = sinon.stub().returns(returnedInstance);


	(function becauseOf(){
		var mapper = Bifrost.mapping.mapper.create(parameters);
		mappedInstance = mapper.map(type, data);
	})();

	it("should return the instance", function () {
		expect(mappedInstance).toEqual(returnedInstance);
	});

	it("should map the corresponding objectProperty", function(){
		expect(mappedInstance.objectProperty).toEqual(data.objectProperty);
	});

	it("should map the objects properties value", function() {
		expect(mappedInstance.objectProperty.objectProperty).toEqual("value")
	});

});