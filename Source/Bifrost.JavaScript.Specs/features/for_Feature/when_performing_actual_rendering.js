describe("when performing actual rendering", function() {
	var parent;
	var featureManagerHookupCalled = false;
	Bifrost.features.featureManager = {
		hookup : function() {
			featureManagerHookupCalled = true;
		}
	}
	Bifrost.navigation = {
		navigationManager : {
			hookup : function(parentInput) {
				parent = parentInput;
			}
		}
	}
	
	var target = $("<div/>");
	
	var feature = Bifrost.features.Feature.create("MyFeature","/",false);
	feature.actualRenderTo(target[0]);

	it("should hook up featureManager", function() {
		expect(featureManagerHookupCalled).toBe(true);
	});
	
	it("should hook up with navigation manager with target as parent", function() {
		expect(parent).toBe(target[0])
	});
});