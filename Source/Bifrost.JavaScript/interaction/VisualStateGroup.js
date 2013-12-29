Bifrost.namespace("Bifrost.interaction", {
	VisualStateGroup: Bifrost.Type.extend(function() {
		/// <summary>Represents a group that holds visual states</summary>
		var self = this;

		this.defaultDuration = Bifrost.TimeSpan.zero();

		/// <field name="currentState" type="Bifrost.interaction.VisualState">Holds the current state, this is an observable</field>
		this.currentState = ko.observable();

		/// <field name="states" type="Array" elementType="Bifrost.interaction.VisualState">Holds an observable array of visual states</field>
		this.states = ko.observableArray();

		/// <field name="transitions" type="Array" elementType="Bifrost.interaction.VisualStateTransition">Holds an observable array of visual state transitions</field>
		this.transitions = ko.observableArray();

		this.addState = function(state) {
			/// <summary>Add a state to the group</summary>
			/// <param name="state" type="Bifrost.interaction.VisualState">State to add</param>
			if( self.hasState(state.name) ) {
				throw "VisualState with name of '"+state.name+"' already exists";
			}
			self.states.push(state);
		};

		this.addTransition = function(transition) {
			/// <summary>Add transition to group</summary>
			/// <param name="transition" type="Bifrost.interaction.VisualStateTransition">Transition to add</param>
			self.transitions.push(transition);
		};

		this.hasState = function(stateName) {
			/// <summary>Check if group has state by the name of the state</summary>
			/// <param name="stateName" type="String">Name of the state to check for</param>
			/// <returns type="Boolean">True if the state exists, false if not</returns>
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
			/// <summary>Gets a state by its name</summary>
			/// <param name="stateName" type="String">Name of the state to get</param>
			/// <returns type="Bifrost.interaction.VisualState">State found or null if it does not have a state by the given name</returns>
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
			/// <summary>Go to a specific state by the name of the state</summary>
			/// <param name="stateName" type="String">Name of the state to go to</param>
			var state = self.getStateByName(stateName);
			if( !Bifrost.isNullOrUndefined(state) ) {
				var currentState = self.currentState();
				if( !Bifrost.isNullOrUndefined(currentState) ) {
					currentState.exit(self.defaultDuration);
				}
				self.currentState(state);
				state.enter(self.defaultDuration);
			}
		};x
	})
});