Bifrost.namespace("Bifrost.tasks", {
    Tasks: Bifrost.Type.extend(function (taskHistory) {
        /// <summary>Represents an aggregation of tasks</summary>
        var self = this;

        /// <field name="all" type="Bifrost.tasks.Task">All tasks being executed</field>
        this.all = ko.observableArray();

        /// <field name="errors" type="observableArrayOfString">All errors that occured during execution of the task</field>
        this.errors = ko.observableArray();
        
        /// <field name="isBusy" type="Boolean">Returns true if the system is busy working, false if not</field>
        this.isBusy = ko.computed(function () {
            return self.all().length > 0;
        });

        this.execute = function (task) {
            /// <summary>Adds a task and starts executing it right away</summary>
            /// <param name="task" type="Bifrost.tasks.Task">Task to add</summary>
            /// <returns>A promise to work with for chaining further events</returns>

            var promise = Bifrost.execution.Promise.create();

            self.all.push(task);

            var taskHistoryId = taskHistory.begin(task);

            task.execute().continueWith(function (result) {
                self.all.remove(task);
                taskHistory.end(taskHistoryId, result);
                promise.signal(result);
            }).onFail(function (error) {
                self.all.remove(task);
                taskHistory.failed(taskHistoryId, error);
                promise.fail(error);
            });

            return promise;
        };
    })
});