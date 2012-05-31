describe("when creating without a mapped uri", function () {
	var exception;
	
	beforeEach(function() {
		try {
			Bifrost.features.FeatureMapping.create("something")
		} catch( e ) {
			exception = e;
		}
	});
	
    it("should throw UriNotSpecified", function () {
		expect(exception instanceof Bifrost.features.MappedUriNotSpecified).toBeTruthy();
    });
});