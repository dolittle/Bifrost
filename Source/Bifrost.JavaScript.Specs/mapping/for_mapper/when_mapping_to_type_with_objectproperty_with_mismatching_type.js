describe("when mapping to type with objectproperty with mismatching type", function () {
	var mappedInstance;
	var data = { objectProperty: { objectProperty : 1234}};

	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns("1234")
	    }
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

	it("should map the corresponding numberProprty value", function(){
		expect(mappedInstance.objectProperty.objectProperty).toEqual(data.objectProperty.objectProperty.toString());
	});
});