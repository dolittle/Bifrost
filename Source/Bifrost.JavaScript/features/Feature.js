Bifrost.namespace("Bifrost.features");
Bifrost.features.Feature = (function () {
    var partialViewModelBindingProvider = function () {
        var self = this;

        var originalBindingProvider = ko.bindingProvider.instance;

        this.nodeHasBindings = function (node) {
            var closestViewModel = $(node).closest("[data-feature]");
            if (closestViewModel.length == 1) {
                var viewModelName = closestViewModel.data("feature");
                if (viewModelName == self.currentViewModel) {
                    return originalBindingProvider.nodeHasBindings(node);
                } else {
                    return false;
                }
            }

            return originalBindingProvider.nodeHasBindings(node)
        },

        this.getBindings = function (node, bindingContext) {
            return originalBindingProvider.getBindings(node, bindingContext);
        }
    }



    function Feature(name, path, isDefault) {
        var self = this;
        this.loaded = false;
        this.renderTargets = [];
        this.name = name;
        this.path = path;
        this.isDefault = isDefault;

        if (isDefault) {
            this.viewPath = path + "/view.html";
            this.viewModelpath = path + "/viewModel.js";
        } else {
            this.viewPath = path + ".html";
            this.viewModelpath = path + ".js";
        }

        this.load = function () {
            var actualViewPath = "text!" + self.viewPath + "!strip";
            var actualViewModelPath = self.viewModelpath;

            require([actualViewPath, actualViewModelPath], function (v) {
                self.view = v;

                $.each(self.renderTargets, function (i, r) {
                    self.actualRenderTo(r);
                });

                self.renderTargets = [];

                self.loaded = true;
            });
        }

        this.defineViewModel = function (viewModel, options) {
            self.viewModelDefinition = Bifrost.features.ViewModelDefinition.define(viewModel, options);
        }

        this.renderTo = function (target) {
            if (self.loaded === false) {
                self.renderTargets.push(target);
            } else {
                self.actualRenderTo(target);
            }
        }

        this.actualRenderTo = function (target) {
            $(target).empty();
            $(target).append(self.view);

            if (self.viewModelDefinition) {
                var viewModel = self.viewModelDefinition.getInstance();

                var previousBindingProvider = ko.bindingProvider.instance;
                ko.bindingProvider.instance = new partialViewModelBindingProvider();
                ko.bindingProvider.instance.currentViewModel = self.name;

			    viewModel.onActivated();
                ko.applyBindings(viewModel, target);

                ko.bindingProvider.instance.currentViewModel = "";
                ko.bindingProvider.instance = previousBindingProvider;
            }

            Bifrost.features.featureManager.hookup(function (a) { return $(a, $(target)); });
        }
    }

    return {
        create: function (name, path, isDefault) {
            var feature = new Feature(name, path, isDefault);
            return feature;
        }
    }
})();
