describe("when mapping to instance with matching observable string property", function(){
    var data = { stringProperty: "fourty two" };

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
	    this.stringProperty = ko.observable("");
	});

	var mappedInstance = type.create();

	(function becauseOf(){
		var mapper = Bifrost.mapping.mapper.create(parameters);
		mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should map the corresponding objectProperty", function(){
	    expect(mappedInstance.stringProperty()).toEqual(data.stringProperty);
	});

});