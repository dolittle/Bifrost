Bifrost.features.featureManager.get("Content/index").defineViewModel(function () {
    var self = this;
    this.currentFeature = ko.observableMessage("currentFeatureChanged", "home");
    this.currentFeaturePath = ko.computed(function () {
        var path = self.currentFeature() + "/index";
        return path;
    });
});