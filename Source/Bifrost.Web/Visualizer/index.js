Bifrost.namespace("Bifrost.Visualizer", {
    index: Bifrost.views.ViewModel.extend(function (categories) {
        var self = this;

        this.categories = categories.all().execute();
    })
});