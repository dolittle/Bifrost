describe("when mapping object with matching string property", function(){
	var mappedReadModelInstance;
	var data = { stringProperty: "string"};
	
	var readModelType = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "";
        this.numberProperty = 0;
        this.arrayProperty = [];
        this.objectProperty = {
        	objectProperty : ""
        };
    });

	var returnedInstance = readModelType.create();

    readModelType.create = sinon.stub().returns(returnedInstance);


	(function becauseOf(){
		var readModelMapper = Bifrost.read.readModelMapper.create();
		mappedReadModelInstance = readModelMapper.mapDataToReadModel(readModelType, data);
	})();

	it("should return the instance", function () {
		expect(mappedReadModelInstance).toEqual(returnedInstance);
	});

	it("should map the corresponding stringProprty value", function(){
		expect(mappedReadModelInstance.stringProperty).toEqual(data.stringProperty);
	})

});