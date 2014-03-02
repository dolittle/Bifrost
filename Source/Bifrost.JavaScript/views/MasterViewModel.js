Bifrost.namespace("Bifrost.views", {
    MasterViewModel: Bifrost.Type.extend(function (documentService) {
        var self = this;

        function deactivateViewModel(viewModel) {
            if (!Bifrost.isNullOrUndefined(viewModel)) {
                console.log("Deactivate - if any function"); 
                if (Bifrost.isFunction(viewModel.deactivated)) {
                    viewModel.deactivated();
                }
                console.log("  Delete viewModel");
                delete viewModel;
            }
        }


        function activateViewModel(viewModel) {
            if (!Bifrost.isNullOrUndefined(viewModel) && Bifrost.isFunction(viewModel.activated)) {
                viewModel.activated();
            }
        }

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
            deactivateViewModel(viewModelObservable());

            viewModelObservable(viewModel);

            activateViewModel(viewModel);
        };

        this.clearFor = function (element) {
            var name = documentService.getViewModelNameFor(element);
            if (!self.hasOwnProperty(name)) {
                var viewModelObservable = self[name];
                deactivateViewModel(viewModelObservable());
                viewModelObservable(null);
            }
        };

        this.apply = function () {
            ko.applyBindings(self);
        };
    })
});