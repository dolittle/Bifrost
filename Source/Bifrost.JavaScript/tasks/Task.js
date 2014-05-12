Bifrost.namespace("Bifrost.tasks", {
    Task: Bifrost.Type.extend(function () {
        /// <summary>Represents a task that can be done in the system</summary>
        var self = this;

        /// <field name="errors" type="observableArray">Observable array of errors</field>
        this.errors = ko.observableArray();

        /// <field name="isExceuting" type="boolean">True / false for telling wether or not the task is executing or not</field>
        this.isExecuting = ko.observable(false); 

        this.execute = function () {
            /// <summary>Executes the task</summary>
            /// <returns>A promise</returns>
            var promise = Bifrost.execution.Promise.create();
            promise.signal();
            return promise;
        };

        this.reportError = function (error) {
            /// <summary>Report an error from executing the task</summary>
            /// <param name="error" type="String">Error coming back</param>
            self.errors.push(error);
        };
    })
});