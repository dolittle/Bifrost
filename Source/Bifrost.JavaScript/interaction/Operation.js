Bifrost.namespace("Bifrost.interaction", {
    Operation: Bifrost.Type.extend(function (region, context) {
        /// <summary>Defines an operation that be performed</summary>
        var self = this;

        /// <field name="context" type="Bifrost.interaction.Operation">Context in which the operation exists in</field>
        this.context = context;

        /// <field name="identifier" type="Bifrost.Guid">Unique identifier for the operation instance<field>
        this.identifier = Bifrost.Guid.empty;

        /// <field name="region" type="Bifrost.views.Region">Region that the operation was created in</field>
        this.region = region;

        /// <field name="canPerform" type="observable">Set to true if the operation can be performed, false if not</field>
        this.canPerform = ko.observable(true);
        
        this.perform = function () {
            /// <summary>Function that gets called when an operation gets performed</summary>
            /// <returns>State change, if any - typically helpful when undoing</returns>
            return {};
        };

        this.undo = function (state) {
            /// <summary>Function that gets called when an operation gets undoed</summary>
            /// <param name="state" type="object">State generated when the operation was performed</param>
        };
    })
});