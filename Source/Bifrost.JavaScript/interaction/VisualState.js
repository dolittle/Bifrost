Bifrost.namespace("Bifrost.interaction", {
	VisualState: Bifrost.Type.extend(function() {
		/// <summary>Represents a visual state of a control or element</summary>
		var self = this;

		/// <field name="name" type="String">Name of the visual state</field>
		this.name = "";

		/// <field name="actions" type="Array" elementType="Bifrost.interaction.VisualStateTransitionAction">Transition actions that will execute when transitioning</field>
		this.actions = ko.observableArray();

		this.addAction = function(action) {
			/// <summary>Add action to the visual state</summary>
			/// <param name="action" type="Bifrost.interaction.VisualStateAction">
			self.actions.push(action);
		};

		this.enter = function(namingRoot, duration) {
			/// <summary>Enter the state with a given duration</summary>
			/// <param name="duration" type="Bifrost.TimeSpan">Time to spend entering the state</param>
			self.actions().forEach(function(action) {
				action.onEnter(namingRoot, duration);
			});
		};

		this.exit = function(namingRoot, duration) {
			/// <summary>Exit the state with a given duration</summary>
			/// <param name="duration" type="Bifrost.TimeSpan">Time to spend entering the state</param>
			self.actions().forEach(function(action) {
				action.onExit(namingRoot, duration);
			});
		};
	})
});