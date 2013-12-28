Bifrost.namespace("Bifrost.interaction", {
	VisualStateManager: Bifrost.Type.extend(function() {
		/// <summary>Represents a state manager for dealing with visual states, typically related to a control or other element on a page</summary>
		var self = this;

		/// <field name="groups" type="Array" elementType="Bifrost.interaction.VisualStateGroup">Holds all groups in the state manager</field>
		this.groups = ko.observableArray();

		this.addGroup = function(group) {
			/// <summary>Adds a VisualStateGroup to the manager</summary>
			/// <parameter name="group" type="Bifrost.interaction.VisualStateGroup">Group to add</parameter>
			self.groups.push(group);
		};

		this.goTo = function(stateName) {
			/// <summary>Go to a specific state by its name</summary>
			/// <parameter name="stateName" type="String">Name of state to go to</parameter>
			self.groups().forEach(function(group) {
				if( group.hasState(stateName) == true ) {
					group.goTo(stateName);
				}
			});
		};
	})
});