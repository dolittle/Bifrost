describe("when creating asynchronously with two dependencies", function() {

	Bifrost.dependencyResolver = {
		beginResolve: function(namespace,name) {
			var promise = Bifrost.execution.Promise.create();
			promise.signal(name);
			return promise;
		},
		getDependenciesFor: function() {
			return ["first", "second"];
		}
	}

	var type = Bifrost.Type.define(function(first, second) {
		this.something = "Hello";
		this.first = first;
		this.second = second;
	});


	var result = null;
	type.beginCreate().continueWith(function(nextPromise, parameter) {
		print("Hello");
		result = parameter;
	});

	it("should resolve the first dependency", function() {
		expect(result.first).toBe("first");
	});

});