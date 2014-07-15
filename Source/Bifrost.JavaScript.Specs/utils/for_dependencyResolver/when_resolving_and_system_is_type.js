describe("when resolving and system is type", function() {
	var type = Bifrost.Type.extend(function(dependency) {
		this.something = "Hello";
		this.dependency = dependency;
	});

	var result = null;
	var dependencyResolvers;

	beforeEach(function () {
	    dependencyResolvers = Bifrost.dependencyResolvers;

	    Bifrost.dependencyResolvers = {
	        getAll: function () {
	            return [{
	                canResolve: function () {
	                    return true;
	                },
	                resolve: function (namespace, name) {
	                    if (name == "dependency") {
	                        return "dependency";
	                    }
	                    return type;
	                }
	            }];
	        }
	    };

	    var ns = {};
	    result = Bifrost.dependencyResolver.resolve(ns, "something");
	});

	afterEach(function () {
	    Bifrost.dependencyResolvers = dependencyResolvers;
	});
    

	it("should create instance of type and resolve dependencies", function() {
		expect(result.dependency).toBe("dependency");

	});
});