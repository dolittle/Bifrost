describe("when creating and it has dependencies that require can resolve", function() {
	require = function(dependency) {
		dependencyInput = dependency;
		return { something : "cool"};
	};

	var typeDefinition = function(jQuery) {
		this.something = jQuery.something;
	}
	var f = function() {
		this.typeDefinition	= typeDefinition;
	};

	f.prototype = Bifrost.TypeInfo;
	var typeInfo;
	var instance;
	var dependencyInput;

	
	beforeEach(function() {
		Bifrost.functionParser = {
			parse: function() {
				return ["jQuery"];
			}
		}
		
		typeInfo = new f();
		instance = typeInfo.create();
	});
		
	
	it("should return an instance", function() {
		expect(instance).toBeDefined();
	});
	
	it("should ask require for the dependency", function() {
		expect(dependencyInput).toBe("jQuery")
	});
	
	it("should pass instance as result from require to function", function() {
		expect(instance.something).toBe("cool");
	});
});