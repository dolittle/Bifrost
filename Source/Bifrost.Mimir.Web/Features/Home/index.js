Bifrost.features.featureManager.get("EventViewer/index").defineViewModel(function () {
    var self = this;

    this.refresh = function () {
        eventViewerViewModel.Reload();
    }
});