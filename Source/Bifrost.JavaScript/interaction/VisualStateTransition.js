Bifrost.namespace("Bifrost.interaction", {
    VisualStateTransition: Bifrost.Type.extend(function() {
        /// <summary>Represents a description of transition between two named states</summary>
        var self = this;

        /// <field name="from" type="String">Name of visual state that we are describing transitioning from</field>
        this.from = "";

        /// <field name="to" type="String">Name of visual state that we are describing transitioning to</field>
        this.to = "";

        /// <field name="duration" type="Bifrost.TimeStamp">Duration for the transition</field>
        this.duration = Bifrost.TimeStamp.zero();
    })
});