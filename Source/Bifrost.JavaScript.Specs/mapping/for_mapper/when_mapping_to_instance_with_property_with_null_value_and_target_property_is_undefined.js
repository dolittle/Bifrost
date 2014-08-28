describe("when mapping to instance with property with null value and target property is undefined", function(){
	
	var data = { numberProperty: null};

	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns(42)
	    },
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
		mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should not type convert the value", function () {
	    expect(parameters.typeConverters.convertFrom.called).toBe(false);
	});

	it("should not copy the value across", function () {
	    expect(mappedInstance.numberProperty).toBeUndefined();
	});
});