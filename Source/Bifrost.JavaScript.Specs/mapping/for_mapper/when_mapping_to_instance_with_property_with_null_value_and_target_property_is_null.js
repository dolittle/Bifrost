describe("when mapping to instance with property with null value and target property is null", function(){
	
	var data = { numberProperty: null};

	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns(42)
	    },
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
        this.numberProperty = null;
    });

	var mappedInstance = type.create();
	var exception = null;

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    try {
	        mapper.mapToInstance(type, data, mappedInstance);
	    } catch (ex) {
	        exception = ex;
	    }
	})();

	it("should not throw an exception", function () {
	    expect(exception).toBe(null);
	});

	it("should hold null in the property", function () {
	    expect(mappedInstance.numberProperty).toBe(null);
	});
});