Bifrost.namespace("Bifrost.interaction", {
	VisualState: Bifrost.Type.extend(function() {
		/// <summary>Represents a visual state of a control or element</summary>

		/// <field name="actions" type="Array" elementType="Bifrost.interaction.VisualStateTransitionAction">Transition actions that will execute when transitioning</field>
		this.actions = ko.observableArray();

		this.enter = function(duration) {
			/// <summary>Enter the state with a given duration</summary>
			/// <param name="duration" type="Bifrost.TimeSpan">Time to spend entering the state</param>

		};

		this.exit = function(duration) {

		};

	})
});