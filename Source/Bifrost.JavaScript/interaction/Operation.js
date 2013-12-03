Bifrost.namespace("Bifrost.interaction", {
    Operation: Bifrost.Type.extend(function (region) {
        /// <summary>Defines an operation that be performed</summary>
        var self = this;

        /// <field name="identifier" type="Bifrost.Guid">Unique identifier for the operation instance<field>
        this.identifier = Bifrost.Guid.empty;

        /// <field name="region" type="Bifrost.views.Region">Region that the operation was created in</field>
        this.region = region;

        /// <field name="canPerform" type="observable">Set to true if the operation can be performed, false if not</field>
        this.canPerform = ko.observable(true);
        
        this.perform = function (context) {
            /// <summary>Function that gets called when an operation gets performed</summary>
            /// <param name="context" type="Bifrost.interaction.OperationContext">The context the operation is being performed within</param>
            /// <returns>State change, if any - typically helpful when undoing</returns>
            return {};
        };

        this.undo = function (context, state) {
            /// <summary>Function that gets called when an operation gets undoed</summary>
            /// <param name="context" type="Bifrost.interaction.OperationContext">The context the operation is being undoed within</param>
            /// <param name="state" type="object">State generated when the operation was performed</param>
        };
    })
});