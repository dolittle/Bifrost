describe("when creating and type definition not set", function() {
	Bifrost.functionParser = {
		parse: function() {
		}
	}
	var exception;
	var f = function() {};
	f.prototype = Bifrost.ClassInfo;
	var classInfo = new f();
	try {
		classInfo.create();
	} catch(e) {
		exception = e;
	}
	
	it("should return an instance", function() {
		expect(exception instanceof Bifrost.MissingTypeDefinition).toBeTruthy();
	});
});