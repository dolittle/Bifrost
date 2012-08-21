describe("when hooking up", function() {
	var renderParent;
	var feature;
	
	beforeEach(function() {
		feature = $("<div id='first' data-feature='first'/>");
		$("body")
			.append(feature);
		
		sinon.stub(Bifrost.features.featureManager,"get", function() {
			return {
				renderTo: function(target) {
					renderParent = target;
				}
			}
		})
		Bifrost.features.featureManager.hookup($);
	});
	
	afterEach(function() {
		Bifrost.features.featureManager.get.restore();
	});
	
	it("should render feature to the parent", function() {
		expect(renderParent).toBe(feature[0]);
	});
});