Bifrost.namespace("Bifrost.Visualizer.QualityAssurance", {
    index: Bifrost.views.ViewModel.extend(function (allProblems) {
        var self = this;

        this.allProblems = allProblems.all().execute();


        this.getSeverityImageSrc = function (severity) {
            return "/Bifrost/Visualizer/QualityAssurance/warning.png";
        };
    })
});