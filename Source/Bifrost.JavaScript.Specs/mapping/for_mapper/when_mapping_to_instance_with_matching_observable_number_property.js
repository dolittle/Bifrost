describe("when mapping to instance with matching observable number property", function(){
	var data = { integer: 42, number: 42.2 };

	var parameters = {
	    typeConverters: {}
	};
	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this.integer = ko.observable(0);
        this.number = ko.observable(0.0);
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should map the corresponding integer value", function(){
		expect(mappedInstance.integer()).toEqual(data.integer);
	});

	it("should map the corresponding decimal value", function(){
	    expect(mappedInstance.number()).toEqual(data.number);
	});
});