describe("when mapping object with matching object property", function(){
	var mappedReadModelInstance;
	var data = { objectProperty: {objectProperty : "value"}};
	
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

	it("should map the corresponding objectProperty", function(){
		expect(mappedReadModelInstance.objectProperty).toEqual(data.objectProperty);
	});

	it("should map the objects properties value", function() {
		expect(mappedReadModelInstance.objectProperty.objectProperty).toEqual("value")
	});

});