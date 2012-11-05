Bifrost.namespace("Bifrost.execution", {
	Promise: function() {
		var self = this;

		this.signalled = false;
		this.callback = null;

		this.signal = function(parameter) {
			self.signalled = true;

			self.signalParameter = parameter;

			if( self.callback != null && typeof self.callback !== "undefined" ) {
				self.callback(Bifrost.execution.Promise.create(),self.signalParameter);
			}
		};

		this.continueWith = function(callback) {
			var nextPromise = Bifrost.execution.Promise.create();
			this.callback = callback;

			if( self.signalled === true ) {
				callback(nextPromise,self.signalParameter);
			}
			return nextPromise;
		};
	}
});

Bifrost.execution.Promise.create = function() {
	var promise = new Bifrost.execution.Promise();
	return promise;
};