describe("when mapping to instance with property with null value", function(){
	
	var data = { numberProperty: null};

	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns(42)
	    },
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
        this.numberProperty = 0;
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
		mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should not type convert the value", function () {
	    expect(parameters.typeConverters.convertFrom.called).toBe(false);
	});
});