Bifrost.namespace("Bifrost.tasks", {
    ExecutionTask: Bifrost.tasks.LoadTask.extend(function (files) {
        /// <summary>Represents a base task that represents anything that is executing</summary>
        this.execute = function () {
        }
    })
});