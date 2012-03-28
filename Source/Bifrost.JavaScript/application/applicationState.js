Bifrost.namespace("Bifrost.application");
Bifrost.application.applicationState = (function() {
	this.activeFeatures = [];
	
	return {
		activateFeature: function(feature) {
			this.activeFeatures.push(feature);
		},
		deactivateFeature: function(feature) {
			var index = this.activeFeatures.indexOf(feature);
			this.activeFeatures.splice(index,1);
		},
		reset: function() {
			this.activeFeatures = [];
		},
		activeFeatures: this.activeFeatures
	}
})();