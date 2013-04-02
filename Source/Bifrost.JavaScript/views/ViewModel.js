Bifrost.namespace("Bifrost.views", {
    ViewModel: Bifrost.Type.extend(function () {
        var self = this;
        this.targetViewModel = this;

        this.activated = function () {
            if (typeof self.targetViewModel.onActivated === "function") {
                self.targetViewModel.onActivated();
            }
        };

        this.onCreated = function (lastDescendant) {
            self.targetViewModel = lastDescendant;
        };
    })
});