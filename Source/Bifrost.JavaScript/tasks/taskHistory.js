Bifrost.namespace("Bifrost.tasks", {
    TaskHistoryEntry: Bifrost.Type.extend(function () {
        var self = this;

        this.type = "";
        this.content = "";

        this.begin = ko.observable();
        this.end = ko.observable();
        this.total = ko.computed(function () {
            if (typeof self.end() !== "undefined" && typeof (self.begin()) !== "undefined") {
                return self.end() - self.begin();
            }
            return 0;
        });
        this.result = ko.observable();
        this.error = ko.observable();

        this.isFinished = ko.computed(function () {
            return typeof self.end() !== "undefined";
        });
        this.hasFailed = ko.computed(function () {
            return typeof self.error() !== "undefined";
        });

        this.isSuccess = ko.computed(function () {
            return self.isFinished() && !self.hasFailed();
        });
    }),

    taskHistory: Bifrost.Singleton(function (systemClock) {
        /// <summary>Represents the history of tasks that has been executed since the start of the application</summary>
        var self = this;

        var entriesById = {};

        /// <field param="entries" type="observableArray">Observable array of entries</field>
        this.entries = ko.observableArray();

        this.begin = function (task) {
            var id = Bifrost.Guid.create();
            var entry = Bifrost.tasks.TaskHistoryEntry.create();

            entry.type = task._type._name;

            var content = {};

            for (var property in task) {
                if (property.indexOf("_") != 0 && task.hasOwnProperty(property) && typeof task[property] !== "function") {
                    content[property] = task[property];
                }
            }


            entry.content = JSON.stringify(content);

            entry.begin(systemClock.nowInMilliseconds());
            entriesById[id] = entry;
            self.entries.push(entry);
            return id;
        };

        this.end = function (id, result) {
            if (entriesById.hasOwnProperty(id)) {
                var entry = entriesById[id];
                entry.end(systemClock.nowInMilliseconds());
                entry.result(result);
            }
        };

        this.failed = function (id, error) {
            if (entriesById.hasOwnProperty(id)) {
                var entry = entriesById[id];
                entry.end(systemClock.nowInMilliseconds());
                entry.error(error);
            }
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.taskHistory = Bifrost.tasks.taskHistory;