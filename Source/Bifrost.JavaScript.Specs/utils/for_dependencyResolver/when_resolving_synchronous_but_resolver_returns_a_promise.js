describe("when resolving synchronous but resolver returns a promise", function() {
	var exception;
	var ns = {

	}

	Bifrost.dependencyResolvers = {
		getAll: function() {
			return [{
					canResolve: function() {
						return true;
					},
					resolve: function() {
						return Bifrost.execution.Promise.create();
					}
				}
			];
		}
	};

	try {
		Bifrost.dependencyResolver.resolve(ns,"something");
	} catch(e) {
		exception = e;
	}

	it("should throw an exception", function() {
		expect(exception instanceof Bifrost.AsynchronousDependenciesDetected).toBe(true);
	});
});