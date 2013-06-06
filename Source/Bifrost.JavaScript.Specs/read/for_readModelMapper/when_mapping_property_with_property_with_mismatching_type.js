describe("when mapping property with property with mismatching type", function(){
	var mappedReadModelInstance;
	var data = { numberProperty: "fourty two"};
	
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

	it("should map the corresponding numberProprty value", function(){
		expect(mappedReadModelInstance.numberProperty).toEqual(data.numberProperty);
	});
});