describe("when mapping to instance with property with value and target property is null", function(){
	
	var data = { numberProperty: 42 };

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

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
		mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should not type convert the value", function () {
	    expect(parameters.typeConverters.convertFrom.called).toBe(false);
	});

	it("should copy the value across", function () {
	    expect(mappedInstance.numberProperty).toBe(42);
	});
});