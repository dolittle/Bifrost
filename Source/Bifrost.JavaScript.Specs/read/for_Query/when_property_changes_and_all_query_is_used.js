describe("when property changes", function() {
	var queryExecuted = null;
	var executeCalled = false;
	var queryServiceMock = {
		execute: function(query) {}
	};
	var query = Bifrost.read.Query.extend(function() {
		this.someInteger = ko.observable(0);
		this.someString = ko.observable("something");
	});
	


	var instance = query.create({
		queryService: queryServiceMock
	});

	var all = instance.all();

	queryServiceMock.execute = function(query) {
		executeCalled = true;
		queryExecuted = query;
	};

	instance.someInteger(42);
	instance.someString("something else");

	it("should call execute on the query service", function() {
		expect(executeCalled).toBe(true);
	});

	it("should forward the query instance to the query service", function() {
		expect(queryExecuted).toBe(instance);
	});
});