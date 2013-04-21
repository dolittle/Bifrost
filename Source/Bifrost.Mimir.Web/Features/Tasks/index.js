Bifrost.features.featureManager.get("Tasks/index").defineViewModel(function () {
    var self = this;

    this.tasks = ko.observableArray();

    this.setTask = function (task) {
        var foundIndex = -1;
        $.each(self.tasks(), function (index, currentTask) {
            if (currentTask.Id() == task.Id) {
                foundIndex = index;
                return false;
            }
        });

        if (foundIndex >= 0) {
            ko.mapping.fromJS(task, self.tasks()[foundIndex]);
        } else {
            self.tasks.push(ko.mapping.fromJS(task));
        }
    };
    this.removeTask = function (task) {
        var foundIndex = -1;
        $.each(self.tasks(), function (index, currentTask) {
            if (currentTask.Id() == task.Id) {
                foundIndex = index;
                return false;
            }
        });

        if (foundIndex >= 0) {
            self.tasks.remove(self.tasks()[foundIndex]);
        }
    };

    var connection = $.hubConnection();
    var tasksHub = connection.createHubProxy("tasks");

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
    tasksHub.on("stateChanged", function (task) {
        self.setTask(task);
    });
    tasksHub.on("stopped", function (task) {
        self.removeTask(task);
    });


    this.showTask = function (elem) {
        if (elem.nodeType === 1) $(elem).hide().slideDown()
    }

    this.hideTask = function (elem) {
        if (elem.nodeType === 1) $(elem).slideUp(function () { $(elem).remove(); })
    }
});

ko.bindingHandlers.task = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var header = $($("header", $(element))[0]);
        var type = ko.utils.unwrapObservable(valueAccessor().Type);
        var template = $("#" + type)[0];
        if (typeof template != "undefined") {
            var $template = $("<div/>").append($($(template).html()));
            var headerInTemplate = $("header", $template)[0];
            if (typeof headerInTemplate != "undefined") {
                $("span", header).remove()
                headerTitle = $("<span/>").append($(headerInTemplate).html());
                header.append(headerTitle);
            }

            var content = $(".content", $(element));
            var contentInTemplate = $(".content", $template)[0];
            if( typeof contentInTemplate != "undefined") {
                content.html($(contentInTemplate).html());
            }
        }
    },
    update: function (element, valueAccessor, allBindingAccessor, viewModel) {
    }
};
