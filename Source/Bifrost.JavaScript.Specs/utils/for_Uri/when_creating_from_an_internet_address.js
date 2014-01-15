describe("when creating from an internet address", function() {
	var location = "http://www.vg.no:8081/some/route#!some/anchor?firstParameter=5&secondParameter=horse";
	var uri = Bifrost.Uri.create(location);
	
	it("should have scheme set", function() {
		expect(uri.scheme).toBe("http");
	});
	
	it("should have host set", function() {
		expect(uri.host).toBe("www.vg.no");
	});
	
	it("should have path set", function() {
		expect(uri.path).toBe("/some/route/some/anchor");
	});
	
	it("should have fullPath set", function() {
		expect(uri.fullPath).toBe(location);
	});
	
	it("should have queryString set", function() {
		expect(uri.queryString).toBe("firstParameter=5&secondParameter=horse");
	});
	
	it("should have port set", function() {
		expect(uri.port).toBe(8081);
	});
	
	it("should set parameters", function() {
		expect(uri.parameters.firstParameter).toBe(5);
		expect(uri.parameters.secondParameter).toBe("horse");
	});
});