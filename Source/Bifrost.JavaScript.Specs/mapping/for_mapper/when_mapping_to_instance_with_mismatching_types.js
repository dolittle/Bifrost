describe("when mapping to instance with mismatching types", function(){
	var data = { integer: "42" };

	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns(42)
	    },
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	
	var type = Bifrost.Type.extend(function () {
        this.integer = 0;
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should type convert the value", function(){
	    expect(parameters.typeConverters.convertFrom.calledWith(data.integer, "Number")).toBe(true);
	});

	it("should set the converted value to the property", function () {
	    expect(mappedInstance.integer).toEqual(42);
	});

});