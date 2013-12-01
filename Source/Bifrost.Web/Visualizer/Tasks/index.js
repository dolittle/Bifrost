Bifrost.namespace("Bifrost.Visualizer.Tasks", {
    index: Bifrost.views.ViewModel.extend(function (taskHistory) {
        var self = this;

        this.taskHistory = taskHistory;

        this.currentEntry = ko.observable().extend({ throttle: 500 });;

        this.tooltipXPosition = ko.observable();
        this.tooltipYPosition = ko.observable();

        function scale(value) {
            return value / 10;
        }

        this.getTotalFormatted = function (entry) {
            return entry.total().toFixed(2);
        };

        this.getOffsetFor = function (entry) {
            var firstEntry = self.taskHistory.entries()[0];
            var zero = firstEntry.begin();

            var offset = entry.begin() - zero;

            return scale(offset) + "px";
        };

        this.getWidthFor = function (entry) {
            var delta = entry.total();

            return scale(delta) + "px";
        };

        this.entryHovered = function (entry, e) {
            var xposition = e.pageX;
            var yposition = e.pageY;

            self.tooltipXPosition(xposition + "px");
            self.tooltipYPosition(yposition + "px");
        };

        this.entryEntered = function (entry, e) {
            var xposition = e.pageX;
            var yposition = e.pageY;

            self.tooltipXPosition(xposition + "px");
            self.tooltipYPosition(yposition + "px");

            self.currentEntry(entry);
        };

        this.entryLeft = function () {
            self.currentEntry(undefined);
        };
    })
});