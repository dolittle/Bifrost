describe("when mapping to instance with matching system property", function(){
	var data = { _name: "something" };

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	
	var type = Bifrost.Type.extend(function () {
        var self = this;

        this._name = "something else";
        this.decimal = 0.0;
    });

	var mappedInstance = type.create();

	(function becauseOf(){
	    var mapper = Bifrost.mapping.mapper.create(parameters);
	    mapper.mapToInstance(type, data, mappedInstance);
	})();

	it("should not map the corresponding decimal value", function(){
		expect(mappedInstance._name).not.toEqual(data._name);
	});
});