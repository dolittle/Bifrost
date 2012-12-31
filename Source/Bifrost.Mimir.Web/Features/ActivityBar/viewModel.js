Bifrost.features.featureManager.get("ActivityBar").defineViewModel(function () {
    var self = this;

    this.tasks = ko.observableArray();

    this.setTask = function (task) {
        var foundIndex = -1;
        $.each(self.tasks(), function (index, task) {
            if (task.id == task.id) {
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
        $.each(self.tasks(), function (index, task) {
            if (task.id == task.id) {
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
    tasksHub.on("started", function (task) {
        self.setTask(task);
    });
    tasksHub.on("stopped", function (task) {
        self.removeTask(task);
    });

    connection.start();
});