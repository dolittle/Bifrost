Bifrost.namespace("Bifrost.interaction", {
    Operations: Bifrost.Type.extend(function (operationEntryFactory) {
        /// <summary>Represents a stack of operations and the ability to perform and put operations on the stack</summary>
        var self = this;

        /// <field name="all" type="observableArray">Holds all operations</field>
        this.all = ko.observableArray();

        this.perform = function (context, operation) {
            /// <summary>Perform an operation in a given context</summary>
            /// <param name="context" type="Bifrost.interaction.OperationContext">Context in which the operation is being performed in</param>
            /// <param name="operation" type="Bifrost.interaction.Operation">Operation to perform</param>


            if (operation.canPerform() === true) {
                var state = operation.perform(context);
                var entry = operationEntryFactory.create(context, operation, state);
                self.all.push(entry);
            }
        };

        this.undo = function () {
            /// <summary>Undo the last operation on the stack and remove it as an operation</summary>

            throw "Not implemented";
        }
    })
});