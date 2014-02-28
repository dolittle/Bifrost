Bifrost.namespace("Bifrost.views", {
    MasterViewModel: Bifrost.Type.extend(function (documentService) {
        var self = this;

        this.getViewModelObservableFor = function (element) {
            var name = documentService.getViewModelNameFor(element);

            var observable = null;
            if (self.hasOwnProperty(name)) {
                observable = self[name]
            } else {
                observable = ko.observable();
                observable.__bifrost_vm__ = name;
                self[name] = observable;
            }
            return observable;
        };

        this.setFor = function (element, viewModel) {
            var viewModelObservable = self.getViewModelObservableFor(element);
            var existingViewModel = viewModelObservable();
            if (!Bifrost.isNullOrUndefined(existingViewModel)) {
                if (Bifrost.isFunction(existingViewModel.deactivated)) {
                    existingViewModel.deactivated();
                }
            }

            viewModelObservable(viewModel);

            if (Bifrost.isFunction(viewModel.activated)) {
                viewModel.activated();
            }
        };

        this.clearFor = function (element) {
            var name = documentService.getViewModelNameFor(element);
            if (!self.hasOwnProperty(name)) {
                self[name](null);
            }
        };

        this.apply = function () {
            ko.applyBindings(self);
        };
    })
});