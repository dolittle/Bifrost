describe("when mapping and empty object", function(){
	var mappedReadModelInstance;
	var data = {};
	var returnedInstance = {};
	
	var readModelType = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "";
        this.numberProperty = 0;
        this.arrayProperty = [];
        this.objectProperty = {
        	objectProperty : ""
        };
    });

    readModelType.create = sinon.stub().returns(returnedInstance);


	(function becauseOf(){
		var readModelMapper = Bifrost.read.readModelMapper.create();
		mappedReadModelInstance = readModelMapper.mapInstance(readModelType, data);
	})();

	it("should create an instance of the readModelType", function () {
		expect(readModelType.create.called).toBe(true);
	});

	it("should return the instance", function () {
		expect(mappedReadModelInstance).toEqual(returnedInstance);
	});
});