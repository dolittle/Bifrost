First = {};
First.Second = {};

describe("when defining in existing namespace", function() {
	Bifrost.Exception.define("First.Second.SomeException");
	
	it("should put exception type in namespace", function() {
		expect(First.Second.SomeException).toBeDefined();
	});
});