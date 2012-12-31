Bifrost.features.featureManager.get("Tasks/index").defineViewModel(function () {
    var self = this;

    this.tasks = ko.observableArray();

    this.setTask = function (task) {
        var foundIndex = -1;
        $.each(self.tasks(), function (index, currentTask) {
            if (currentTask.Id == task.Id) {
                foundIndex = index;
                return false;
            }
        });

        if (foundIndex >= 0) {
            self.replace(self.tasks()[foundIndex], task);
        } else {
            self.tasks.push(task);
        }
    };
    this.removeTask = function (task) {
        var foundIndex = -1;
        $.each(self.tasks(), function (index, currentTask) {
            if (currentTask.Id == task.Id) {
                foundIndex = index;
                return false;
            }
        });

        if (foundIndex >= 0) {
            self.tasks.remove(self.tasks()[foundIndex]);
        }
    };

    var connection = $.hubConnection();
    var tasksHub = connection.createProxy("tasks");

    connection.start().done(function () {
        tasksHub.invoke("getAllTasks", function (tasks) {
            $.each(tasks, function (index, task) {
                self.setTask(task);
            });
        });
    });

    tasksHub.on("started", function (task) {
        self.setTask(task);
    });
    tasksHub.on("stopped", function (task) {
        self.removeTask(task);
    });



});

/*
ko.bindingHandlers.tasks = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {

        ko.applyBindingsToNode(element, { foreach: valueAccessor() }, viewModel);

        return { controlsDescendantBindings: true };

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
    }
}
*/