Bifrost.namespace("Bifrost.execution", {
	Promise: function() {
		var self = this;

		this.signalSet = false;
		this.callback = null;

		this.signal = function() {
			self.signalSet = true;

			if( self.callback != null && typeof self.callback !== "undefined" ) {
				self.callback();
			}
		};

		this.continueWith = function(callback) {
			this.callback = callback;

			if( self.signalSet === true ) {
				callback();
			}
		};
	}
});

Bifrost.execution.Promise.create = function() {
	var promise = new Bifrost.execution.Promise();
	return promise;
};