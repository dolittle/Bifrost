describe("when resoliving asynchronously and system is type", function() {
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
					var promise = Bifrost.execution.Promise.create();
					var system = type;
					if( name == "dependency") {
						system = "dependency";
					}
					promise.signal(system);
					return promise;
				}
			}];
		}
	};

	var ns = {};
	
	Bifrost.dependencyResolver
				.beginResolve(ns, "something")
				.continueWith(function(next, param) {
					result = param;
				});

	it("should create instance of type and resolve dependencies", function() {
		expect(result.dependency).toBe("dependency");

	});
});