describe("when mapping to instance with read only observable property", function(){
	
	var data = { numberProperty: null};

	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns(42)
	    },
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
	    this.numberProperty = ko.computed(function () {
	        return 42;
	    });
    });

	var exception = null;
    try {
        var mappedInstance = type.create();
    } catch (e) {
        exception = e;
    }

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
		mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should not fail", function () {
	    expect(exception).toBe(null);
	});
});