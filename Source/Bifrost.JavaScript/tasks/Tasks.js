Bifrost.namespace("Bifrost.tasks", {
    Tasks: Bifrost.Type.extend(function (taskHistory) {
        /// <summary>Represents an aggregation of tasks</summary>
        var self = this;

        /// <field name="unfiltered" type="Bifrost.tasks.Task[]">All tasks completely unfiltered</field>
        this.unfiltered = ko.observableArray();

        /// <field name="executeWhen" type="Bifrost.specifications.Specification">Gets or sets the rule for execution</field>
        /// <remarks>
        /// If a task gets executed that does not get satisfied by the rule, it will just queue it up
        /// </remarks>
        this.canExecuteWhen = ko.observable();

        /// <field name="all" type="Bifrost.tasks.Task[]">All tasks being executed</field>
        this.all = ko.computed(function () {
            var all = self.unfiltered();

            var rule = self.canExecuteWhen();

            if (!Bifrost.isNullOrUndefined(rule)) {
                var filtered = [];

                all.forEach(function (task) {
                    rule.evaluate(task);
                    if (rule.isSatisfied() === true) {
                        filtered.push(task);
                    }
                });
                return filtered;
            }

            return all;
        });

        /// <field name="errors" type="observableArrayOfString">All errors that occured during execution of the task</field>
        this.errors = ko.observableArray();

        /// <field name="isBusy" type="Boolean">Returns true if the system is busy working, false if not</field>
        this.isBusy = ko.computed(function () {
            return self.all().length > 0;
        });

        function executeTaskIfNotExecuting(task) {
            if (task.isExecuting() === true) {
                return;
            }
            task.isExecuting(true);
            var taskHistoryId = taskHistory.begin(task);

            task.execute().continueWith(function (result) {
                self.unfiltered.remove(task);
                taskHistory.end(taskHistoryId, result);
                task.promise.signal(result);
            }).onFail(function (error) {
                self.unfiltered.remove(task);
                self.errors.push(task);
                taskHistory.failed(taskHistoryId, error);
                task.promise.fail(error);
            });
        }

        this.all.subscribe(function (changedTasks) {
            changedTasks.forEach(function (task) {
                executeTaskIfNotExecuting(task);
            });
        });

        this.execute = function (task) {
            /// <summary>Adds a task and starts executing it right away</summary>
            /// <param name="task" type="Bifrost.tasks.Task">Task to add</summary>
            /// <returns>A promise to work with for chaining further events</returns>

            task.promise = Bifrost.execution.Promise.create();
            self.unfiltered.push(task);
            
            var rule = self.canExecuteWhen();
            var canExecute = true;
            if (!Bifrost.isNullOrUndefined(rule)) {
                canExecute = rule.evaluate(task);
            }
            
            if (canExecute === true) {
                executeTaskIfNotExecuting(task);
            }
            
            return task.promise;
        };
    })
});