Bifrost.namespace("Bifrost.interaction", {
	VisualStateGroup: Bifrost.Type.extend(function() {
		var self = this;

		this.defaultDuration = Bifrost.TimeSpan.zero();

		this.currentState = ko.observable();
		this.states = ko.observableArray();
		this.transitions = ko.observableArray();

		this.addState = function(state) {
			self.states.push(state);
		};

		this.addTransition = function(transition) {
			self.transitions.push(transition);
		};

		this.hasState = function(stateName) {
			var hasState = false;
			self.states().forEach(function(state) {
				if( state.name === stateName ) {
					hasState = true;
					return;
				}
			});

			return hasState;
		};

		this.getStateByName = function(stateName) {
			var stateFound = null;
			self.states().forEach(function(state) {
				if( state.name === stateName ) {
					stateFound = state;
					return;
				}
			});
			return stateFound;
		};

		this.goTo = function(stateName) {
			var state = self.getStateByName(stateName);
			if( !Bifrost.isNullOrUndefined(state) ) {
				self.currentState(state);
				state.enter(self.defaultDuration);
			}
		};
	})
});