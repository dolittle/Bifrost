describe("when mapping to type with matching system property", function(){
	var mappedInstance;
	var data = { _name: "something" };

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = Bifrost.Type.extend(function () {
        var self = this;

        this._name = "something else";
    });

	var returnedInstance = type.create();

    type.create = sinon.stub().returns(returnedInstance);


	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
		mappedInstance = mapper.map(type, data);
	})();

	it("should return the instance", function () {
		expect(mappedInstance).toEqual(returnedInstance);
	});

	it("should not map the system property across", function(){
		expect(mappedInstance._name).not.toEqual(data._name);
	});
});