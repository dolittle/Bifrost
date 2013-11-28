Bifrost.namespace("Bifrost.tasks", {
    taskHistory: Bifrost.Singleton(function () {
        /// <summary>Represents the history of tasks that has been executed since the start of the application</summary>
        var self = this;

        /// <field param="entries" type="observableArray">Observable array of entries</field>
        this.entries = ko.observableArray();

        this.begin = function (task) {
        };

        this.end = function (task) {
        };

        this.failed = function (task) {
        };
    })
});