describe("when mapping to instance with one of two matching properties", function(){
	var data = { integer: 42, decimal: 42.2 };

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.integer = 0;
    });

	var mappedInstance = type.create();
	var mappedProperties = null;

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mappedProperties = mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should contain the matching property in mapped properties result", function(){
	    expect(mappedProperties.indexOf("integer") >= 0).toBe(true);
	});

	it("should not contain the property without match in mapped properties result", function () {
	    expect(mappedProperties.indexOf("decimal") < 0).toBe(true);
	});
});