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

ko.bindingHandlers.sidebar = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $("body").mouseover(function (e) {
            if ($(e.target).closest("table.bifrostSidebarIcons").length != 1) {
                $("#icons").removeClass("bifrostSidebarWithContent");
                $("#icons").removeClass("bifrostSidebarFullSize");
            }

        });

        $("#sidebar").mouseover(function () {
            $("#icons").addClass("bifrostSidebarIconsVisible");
        });

        $("#sidebar").mouseout(function (e) {
            $("#icons").removeClass("bifrostSidebarIconsVisible");

        });

        $("#icons").mouseover(function () {
            //$("#sidebar").addClass("bifrostSidebarFullSize");
        });

        $("#icons").mouseout(function (e) {
            $("#icons").addClass("bifrostSidebarFullSize");
        });


        $("#icons").click(function () {
            $("#icons").addClass("bifrostSidebarWithContent");
        });
    }
}
