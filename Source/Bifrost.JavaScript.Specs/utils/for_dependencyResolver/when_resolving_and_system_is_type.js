describe("when resolving and system is type", function() {

	var type = Bifrost.Type.define(function(dependency) {
		this.something = "Hello";
		this.dependency = dependency;
	});


	Bifrost.dependencyResolvers = {
		getAll: function() {
			return [{
				canResolve: function() {
					return true;
				},
				resolve: function(namespace, name) {
					if( name == "dependency") {
						return "dependency";
					}
					return type;
				}
			}];
		}
	};

	var ns = {};
	
	var result = Bifrost.dependencyResolver.resolve(ns, "something");

	it("should create instance of type and resolve dependencies", function() {
		expect(result.dependency).toBe("dependency");

	});
});