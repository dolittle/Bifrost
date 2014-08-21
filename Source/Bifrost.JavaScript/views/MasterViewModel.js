Bifrost.namespace("Bifrost.views", {
    MasterViewModel: Bifrost.Type.extend(function (documentService) {
        var self = this;

        function deactivateViewModel(viewModel) {
            if (!Bifrost.isNullOrUndefined(viewModel)) {
                if (Bifrost.isFunction(viewModel.deactivated)) {
                    viewModel.deactivated();
                }
                delete viewModel;
            }
        }


        function activateViewModel(viewModel) {
            if (!Bifrost.isNullOrUndefined(viewModel) && Bifrost.isFunction(viewModel.activated)) {
                viewModel.activated();
            }
        }


        this.setFor = function (element, viewModel) {
            var existingViewModel = self.getFor(element);
            if (existingViewModel !== viewModel) {
                deactivateViewModel(existingViewModel);
            }

            var name = documentService.getViewModelNameFor(element);
            self[name] = viewModel;

            activateViewModel(viewModel);
        };

        this.getFor = function (element) {
            var name = documentService.getViewModelNameFor(element);
            if (self.hasOwnProperty(name)) {
                return self[name];
            }
            return null;
        };


        this.clearFor = function (element) {
            var name = documentService.getViewModelNameFor(element);
            if (!self.hasOwnProperty(name)) {
                deactivateViewModel(self[name]);
                self[name] = null;
            }
        };

        this.apply = function () {
            ko.applyBindings(self);
        };

        this.applyTo = function (element) {
            ko.applyBindings(self, element);
        };
    })
});