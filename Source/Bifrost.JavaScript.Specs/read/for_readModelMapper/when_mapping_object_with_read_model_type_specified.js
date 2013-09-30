globalReadModelType = Bifrost.Type.extend(function () {
    var self = this;
});

describe("when mapping object with read model type specified", function () {
	var mappedReadModelInstance;
	var data = {
	    _readModelType : "globalReadModelType"
	};
	var returnedInstance = {};
	
	var readModelType = Bifrost.Type.extend(function () {
	    var self = this;
	});

    globalReadModelType.create = sinon.stub().returns(returnedInstance);

	(function becauseOf(){
		var readModelMapper = Bifrost.read.readModelMapper.create();
		mappedReadModelInstance = readModelMapper.mapDataToReadModel(readModelType, data);
	})();

	it("should create an instance of the global readModelType", function () {
	    expect(globalReadModelType.create.called).toBe(true);
	});
});