Bifrost.namespace("Bifrost.Debugging.Problems", {
    index: Bifrost.views.ViewModel.extend(function (allProblems) {
        var self = this;

        this.allProblems = allProblems.all().execute();
    })
});