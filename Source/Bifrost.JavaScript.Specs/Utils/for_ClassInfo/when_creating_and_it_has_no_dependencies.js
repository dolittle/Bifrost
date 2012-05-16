describe("when creating and it has no dependencies", function() {
	Bifrost.functionParser = {
		parse: function() {
			return [];
		}
	}
	
	var classDefinition = function() {
		this.something = 42;
	}
	var f = function() {
		this.typeDefinition	= classDefinition;
	};
	f.prototype = Bifrost.ClassInfo;
	var classInfo = new f();
	var instance = classInfo.create();
	
	
	it("should return an instance", function() {
		expect(instance).toBeDefined();
	});
	
});