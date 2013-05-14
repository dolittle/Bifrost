describe("when mapping array of data", function(){
	var mappedReadModelInstance;
	var data = [{ stringProperty: "fourty two"}, {stringProperty: "fourty three"}];
	
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

	(function becauseOf(){
		var readModelMapper = Bifrost.read.readModelMapper.create();
		mappedReadModelInstance = readModelMapper.mapDataToReadModel(readModelType, data);
	})();

	it("should return an array of mapped instances", function () {
		expect(mappedReadModelInstance.length).toEqual(2);
	});

	it("should map the first datas properties", function(){
		expect(mappedReadModelInstance[0].stringProperty).toEqual("fourty two");
	});

	it("should map the second datas properties", function(){
		expect(mappedReadModelInstance[1].stringProperty).toEqual("fourty three");
	});

});