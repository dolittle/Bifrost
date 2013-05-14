describe("when mapping object with objectproperty with mismatching type", function(){
	var mappedReadModelInstance;
	var data = { objectProperty: { objectProperty : 1234}};
	
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
		mappedReadModelInstance = readModelMapper.mapInstance(readModelType, data);
	})();

	it("should return the instance", function () {
		expect(mappedReadModelInstance).toEqual(returnedInstance);
	});

	it("should not map the corresponding numberProprty value", function(){
		expect(mappedReadModelInstance.objectProperty.objectProperty).toEqual("");
	});

});