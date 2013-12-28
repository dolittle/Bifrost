Bifrost.namespace("Bifrost.interaction", {
	VisualStateTransition: Bifrost.Type.extend(function() {
		var self = this;

		this.from = "";
		this.to = "";
		this.duration = Bifrost.TimeStamp.zero();
	})
});