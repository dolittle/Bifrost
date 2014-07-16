describe("when mapping object with matching string property", function(){
	var mappedInstance;
	var data = { stringProperty: "string"};
	
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
	    var mapper = Bifrost.mapping.mapper.create();
		mappedInstance = mapper.map(type, data);
	})();

	it("should return the instance", function () {
		expect(mappedInstance).toEqual(returnedInstance);
	});

	it("should map the corresponding stringProprty value", function(){
		expect(mappedInstance.stringProperty).toEqual(data.stringProperty);
	})

});