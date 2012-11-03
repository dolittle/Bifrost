Bifrost.namespace("Bifrost.execution", {
	Promise: function() {
		var self = this;

		this.signalled = false;
		this.callback = null;

		this.signal = function() {
			self.signalled = true;

			if( self.callback != null && typeof self.callback !== "undefined" ) {
				self.callback(Bifrost.execution.Promise.create());
			}
		};

		this.continueWith = function(callback) {
			this.callback = callback;

			if( self.signalled === true ) {
				callback(Bifrost.execution.Promise.create());
			}
		};
	}
});

Bifrost.execution.Promise.create = function() {
	var promise = new Bifrost.execution.Promise();
	return promise;
};