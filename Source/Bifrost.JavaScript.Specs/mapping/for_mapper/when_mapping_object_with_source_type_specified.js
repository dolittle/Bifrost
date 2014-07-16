globalType = Bifrost.Type.extend(function () {
    var self = this;
});

describe("when mapping object with source type specified", function () {
	var mappedInstance;
	var data = {
	    _sourceType : "globalType"
	};
	var returnedInstance = {};
	
	var type = {
	    create: sinon.stub().returns({
	    })
	}

    globalType.create = sinon.stub().returns(returnedInstance);

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create();
		mappedInstance = mapper.map(type, data);
	})();

	it("should create an instance of the specified source type on the data", function () {
	    expect(globalType.create.called).toBe(true);
	});

	it("should not create an instance of the type passed in", function () {
	    expect(type.create.called).toBe(false);
	});
});