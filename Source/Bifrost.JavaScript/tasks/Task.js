Bifrost.namespace("Bifrost.tasks", {
    Task: Bifrost.Type.extend(function () {
        /// <summary>Represents a task that can be done in the system</summary>
        var self = this;

        this.execute = function () {
            /// <summary>Executes the task</summary>
            /// <returns>A promise</returns>
            var promise = Bifrost.execution.Promise.create();
            promise.signal();
            return promise;
        };
    })
});