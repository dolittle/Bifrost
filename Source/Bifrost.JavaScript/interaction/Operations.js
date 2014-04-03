Bifrost.namespace("Bifrost.interaction", {
    Operations: Bifrost.Type.extend(function (operationEntryFactory) {
        /// <summary>Represents a stack of operations and the ability to perform and put operations on the stack</summary>
        var self = this;

        /// <field name="all" type="observableArray">Holds all operations</field>
        this.all = ko.observableArray();

        /// <field name="stateful" type="observableArray">Holds all operations that are stateful - meaning that they produce state from being performed</field>
        this.stateful = ko.computed(function () {
            var entries = [];

            self.all().forEach(function (entry) {
                if (!Bifrost.areEqual(entry.state, {})) {
                    entries.push(entry);
                }
            });

            return entries;
        });

        this.getByIdentifier = function (identifier) {
            /// <summary>Get an operation by its identifier</identifier>
            /// <param name="identifier" type="Bifrost.Guid">Identifier of the operation to get<param>
            /// <returns>An instance of the operation if found, null if not found</returns>

            var found = null;
            self.all().forEach(function (operation) {
                if (operation.identifier === identifier) {
                    found = operation;
                    return;
                }
            });

            return found;
        };

        this.perform = function (operation) {
            /// <summary>Perform an operation in a given context</summary>
            /// <param name="context" type="Bifrost.interaction.OperationContext">Context in which the operation is being performed in</param>
            /// <param name="operation" type="Bifrost.interaction.Operation">Operation to perform</param>

            if (operation.canPerform() === true) {
                var state = operation.perform();
                var entry = operationEntryFactory.create(operation, state);
                self.all.push(entry);
            }
        };

        this.undo = function () {
            /// <summary>Undo the last operation on the stack and remove it as an operation</summary>

            throw "Not implemented";
        }
    })
});