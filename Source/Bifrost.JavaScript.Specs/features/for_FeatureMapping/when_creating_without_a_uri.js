describe("when creating without a uri", function () {
	var exception;
	
	beforeEach(function() {
		try {
			Bifrost.features.FeatureMapping.create()
		} catch( e ) {
			exception = e;
		}
	});
	
    it("should throw UriNotSpecified", function () {
		expect(exception instanceof Bifrost.features.UriNotSpecified).toBeTruthy();
    });
});