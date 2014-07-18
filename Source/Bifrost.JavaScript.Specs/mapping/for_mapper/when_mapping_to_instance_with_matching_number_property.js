describe("when mapping to instance with matching number property", function(){
	var data = { integer: 42, decimal: 42.2 };

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.integer = 0;
        this.decimal = 0.0;
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should map the corresponding integer value", function(){
		expect(mappedInstance.integer).toEqual(data.integer);
	});

	it("should map the corresponding decimal value", function(){
		expect(mappedInstance.decimal).toEqual(data.decimal);
	});

});