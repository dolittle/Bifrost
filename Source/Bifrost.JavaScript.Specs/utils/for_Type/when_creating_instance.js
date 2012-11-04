describe("when creating instance", function() {

	var type = null;
	var instance = null;
	beforeEach(function() {
		Bifrost.dependencyResolver = {
			getDependenciesFor: sinon.stub()
		};

		type = Bifrost.Type.define(function() {
		});
		instance = type.create();
	});

	afterEach(function() {
		Bifrost.dependencyResolver = {};
	});
	
	it("should return an instance", function() {
		expect(instance).not.toBeNull();
	});
});