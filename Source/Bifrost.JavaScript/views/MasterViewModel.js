Bifrost.namespace("Bifrost.views", {
    MasterViewModel: Bifrost.Type.extend(function (documentService) {
        var self = this;

        function getNameFrom(viewModel) {
            var fullName = viewModel._type._namespace.name + "." + viewModel._type._name;
            return fullName;
        }


        this.set = function (viewModel) {
            var name = getNameFrom(viewModel);
            if (self.hasOwnProperty(name)) {
                var existingViewModel = self[name]();
                if (!Bifrost.isNullOrUndefined(existingViewModel)) {
                    if (Bifrost.isFunction(existingViewModel.deactivated)) {
                        existingViewModel.deactivated();
                    }
                }

                self[name](viewModel);
            } else {
                self[name] = ko.observable(viewModel);
            }

            if (Bifrost.isFunction(viewModel.activated)) {
                viewModel.activated();
            }
        };

        this.applyBindingExpressionForViewModel = function (element, viewModel) {
            var name = getNameFrom(viewModel);
            self.set(viewModel);
            documentService.setViewModelOn(element, viewModel);
            documentService.setViewModelBindingExpression(element, "$data['" + name + "']");
        };

        this.applyBindingForViewModel = function (element, viewModel) {
            self.set(viewModel);
            documentService.setViewModelOn(element, viewModel);
            ko.applyBindingsToNode(element, {
                'viewModel': viewModel
            });
            
        };
    })
});