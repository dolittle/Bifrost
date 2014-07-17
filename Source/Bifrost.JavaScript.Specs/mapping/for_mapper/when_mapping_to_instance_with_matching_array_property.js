describe("when mapping to type with matching array property", function () {
    var data = { arrayProperty: ["value1", "value2"] };

    var parameters = {
        typeConverters: {}
    };
	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.stringProperty = "s";
        this.numberProperty = 0;
        this.arrayProperty = [];
        this.objectProperty = {
        	objectProperty : ""
        };
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mapper.mapToInstance(type, data, mappedInstance);
	})();


	it("should map the corresponding arrayProperty value", function(){
		expect(mappedInstance.arrayProperty).toEqual(data.arrayProperty);
	});

});