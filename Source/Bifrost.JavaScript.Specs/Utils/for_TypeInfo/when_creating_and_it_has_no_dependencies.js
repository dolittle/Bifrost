describe("when creating and it has no dependencies", function() {
	Bifrost.functionParser = {
		parse: function() {
			return [];
		}
	}
	
	var typeDefinition = function() {
		this.something = 42;
	}
	var f = function() {
		this.typeDefinition	= typeDefinition;
	};
	f.prototype = Bifrost.TypeInfo;
	var typeInfo = new f();
	var instance = typeInfo.create();
	
	it("should return an instance", function() {
		expect(instance).toBeDefined();
	});
	
});