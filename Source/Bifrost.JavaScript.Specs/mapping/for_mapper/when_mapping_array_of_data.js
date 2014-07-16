describe("when mapping array of data", function(){
	var mappedInstance;
	var data = [{ stringProperty: "fourty two"}, {stringProperty: "fourty three"}];
	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "";
        this.numberProperty = 0;
        this.arrayProperty = [];
        this.objectProperty = {
        	objectProperty : ""
        };
    });

	var returnedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create();
		mappedInstance = mapper.map(type, data);
	})();

	it("should return an array of mapped instances", function () {
		expect(mappedInstance.length).toEqual(2);
	});

	it("should map the first datas properties", function(){
		expect(mappedInstance[0].stringProperty).toEqual("fourty two");
	});

	it("should map the second datas properties", function(){
		expect(mappedInstance[1].stringProperty).toEqual("fourty three");
	});

});