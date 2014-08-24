Bifrost.namespace("Bifrost.interaction", {
    operationEntryFactory: Bifrost.Singleton(function () {
        /// <summary>Represents a factory that can create OperationEntries</summary>

        this.create = function (operation, state) {
            /// <sumary>Create an instance of a OperationEntry</summary>
            /// <param name="context" type="Bifrost.interaction.OperationContext">Context the operation was performed in</param>
            /// <param name="operation" type="Bifrost.interaction.Operation">Operation that was performed</param>
            /// <param name="state" type="object">State that operation generated</param>
            /// <returns>An OperationEntry</returns>

            var instance = Bifrost.interaction.OperationEntry.create({
                operation: operation,
                state: state
            });
            return instance;
        };
    })
});