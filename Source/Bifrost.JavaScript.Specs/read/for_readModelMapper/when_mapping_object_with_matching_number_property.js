describe("when mapping object with matching number property", function(){
	var mappedReadModelInstance;
	var data = { numberProperty: 42};
	
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

	it("should map the corresponding numberProprty value", function(){
		expect(mappedReadModelInstance.numberProperty).toEqual(data.numberProperty);
	});

});