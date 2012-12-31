Bifrost.features.featureManager.get("ActivityBar").defineViewModel(function () {
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
    tasksHub.on("started", function (task) {
        self.setTask(task);
    });
    tasksHub.on("stopped", function (task) {
        self.removeTask(task);
    });

    connection.start();

    this.tasks.push({ Id: 12314323, Type: "DoStuff" });
    this.tasks.push({ Id: 123123, Type: "DoOtherStuff" });

    this.showTask = function (elem) { if (elem.nodeType === 1) $(elem).hide().slideDown() }
    this.hideTask = function (elem) { if (elem.nodeType === 1) $(elem).slideUp(function () { $(elem).remove(); }) }

});