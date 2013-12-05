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
    })
});