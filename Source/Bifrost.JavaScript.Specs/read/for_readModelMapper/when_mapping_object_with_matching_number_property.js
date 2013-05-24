describe("when mapping object with matching number property", function(){
	var mappedReadModelInstance;
	var data = { integer: 42, decimal: 42.2 };
	
	var readModelType = Bifrost.Type.extend(function () {
        var self = this;

        this.integer = 0;
        this.decimal = 0.0;
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

	it("should map the corresponding integer value", function(){
		expect(mappedReadModelInstance.integer).toEqual(data.integer);
	});

	it("should map the corresponding decimal value", function(){
		expect(mappedReadModelInstance.decimal).toEqual(data.decimal);
	});

});