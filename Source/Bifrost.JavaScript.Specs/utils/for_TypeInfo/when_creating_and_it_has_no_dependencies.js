describe("when creating and it has no dependencies", function() {
	var typeDefinition = function() {
		this.something = 42;
	}
	var f = function() {
		this.typeDefinition	= typeDefinition;
	};

	f.prototype = Bifrost.TypeInfo;
	var typeInfo;
	var instance;
	
	beforeEach(function() {
		Bifrost.functionParser = {
			parse: function() {
				return [];
			}
		}
		
		typeInfo = new f();
		instance = typeInfo.create();
	});
	
	it("should return an instance", function() {
		expect(instance).toBeDefined();
	});
});