describe("when mapping object with matching number property", function(){
	var mappedInstance;
	var data = { integer: 42, decimal: 42.2 };
	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.integer = 0;
        this.decimal = 0.0;
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

	it("should map the corresponding integer value", function(){
		expect(mappedInstance.integer).toEqual(data.integer);
	});

	it("should map the corresponding decimal value", function(){
		expect(mappedInstance.decimal).toEqual(data.decimal);
	});

});