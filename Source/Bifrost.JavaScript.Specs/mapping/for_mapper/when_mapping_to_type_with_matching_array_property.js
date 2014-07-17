describe("when mapping to type with matching array property", function () {
	var mappedInstance;
	var data = { arrayProperty: [ "value1", "value2"]};

	var parameters = {
	    typeConverters: {}
	};

	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "s";
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

	it("should map the corresponding arrayProperty value", function(){
		expect(mappedInstance.arrayProperty).toEqual(data.arrayProperty);
	});

});