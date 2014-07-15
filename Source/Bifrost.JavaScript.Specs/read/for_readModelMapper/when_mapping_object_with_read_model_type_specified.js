globalReadModelType = Bifrost.Type.extend(function () {
    var self = this;
});

describe("when mapping object with read model type specified", function () {
	var mappedReadModelInstance;
	var data = {
	    _readModelType : "globalReadModelType"
	};
	var returnedInstance = {};
	
	var readModelType = {
	    create: sinon.stub().returns({
	    })
	}

    globalReadModelType.create = sinon.stub().returns(returnedInstance);

	(function becauseOf(){
		var readModelMapper = Bifrost.read.readModelMapper.create();
		mappedReadModelInstance = readModelMapper.mapDataToReadModel(readModelType, data);
	})();

	it("should create an instance of the specified readModelType on the data", function () {
	    expect(globalReadModelType.create.called).toBe(false);
	});

	it("should not create an instance of the readModelType passed in", function () {
	    expect(readModelType.create.called).toBe(true);
	});
});