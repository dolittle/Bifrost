describe("when mapping to instance with matching observable number property", function(){
	var data = { date: "some date" };
	var date = new Date();
	var parameters = {
	    typeConverters: {
	        convertFrom: sinon.stub().returns(date)
	    },
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};
	
	var type = Bifrost.Type.extend(function () {
	    this.date = ko.observable(0);
	    this.date._typeAsString = "Date";
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should type convert the value", function(){
	    expect(parameters.typeConverters.convertFrom.calledWith(data.date, "Date")).toBe(true);
	});

	it("should set the converted value to the property", function(){
	    expect(mappedInstance.date()).toEqual(date);
	});
});