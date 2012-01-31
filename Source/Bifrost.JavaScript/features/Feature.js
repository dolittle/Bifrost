Bifrost.namespace("Bifrost.features");
Bifrost.features.Feature = (function () {
    function Feature(name, view, viewModel, path) {

        this.name = name;
        this.view = view;
        this.viewModel = viewModel;
        this.path = path;

        var self = this;

        this.load = function (loaded) {

            //var path = "/Features/"+self.name;
            var view = "text!" + self.path + "/" + self.view + ".html!strip";
            //var styles = "text!"+path+"/views.css";
            var viewModelPath = self.path + "/" + self.viewModel + ".js";

            //			require([view, styles, viewModelPath], function(v,s) {
            require([view, viewModelPath], function (v) {
                self.view = v;
                //self.stylesheet = s;

                if (loaded) {
                    loaded(self);
                }
            });
        }

        this.defineViewModel = function (viewModel, isSingleton) {
            self.viewModel = Bifrost.features.ViewModel.create(viewModel, isSingleton);
        }

        this.renderTo = function (target) {
            $(target).append(self.view);
            var viewModel = self.viewModel.getInstance();
            ko.applyBindings(viewModel, target);
        }
    }

    return {
        create: function (name, view, viewModel, path) {
            var feature = new Feature(name, view, viewModel, path);
            return feature;
        }
    }
})();
