Bifrost.namespace("Bifrost.Visualizer", {
    index: Bifrost.views.ViewModel.extend(function () {
        var self = this;

        this.categories = [
            { name: "QualityAssurance", displayName: "Quality Assurance", description: "" },
            { name: "Tasks", displayName: "Tasks", description: "" }
        ];

        this.currentCategory = ko.observable("Visualizer/"+this.categories[0].name+"/index");

        this.selectCategory = function (category) {
            self.currentCategory("Visualizer/" + category.name + "/index");
        };
    })
});