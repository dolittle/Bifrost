describe("when mapping object with matching array property", function(){
	var mappedReadModelInstance;
	var data = { arrayProperty: [ "value1", "value2"]};
	
	var readModelType = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "s";
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
		mappedReadModelInstance = readModelMapper.mapInstance(readModelType, data);
	})();

	it("should return the instance", function () {
		expect(mappedReadModelInstance).toEqual(returnedInstance);
	});

	it("should map the corresponding arrayProperty value", function(){
		expect(mappedReadModelInstance.arrayProperty).toEqual(data.arrayProperty);
	});

});