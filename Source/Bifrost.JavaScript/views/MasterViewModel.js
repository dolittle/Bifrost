Bifrost.namespace("Bifrost.views", {
    MasterViewModel: Bifrost.Type.extend(function (documentService) {
        var self = this;

        function getNameFrom(viewModel) {
            var fullName = viewModel._type._namespace.name + "." + viewModel._type._name;
            return fullName;
        }

        this.getViewModelObservableFor = function (element) {
            var name = "";
            var alreadySet = false;
            if (Bifrost.isNullOrUndefined(element.__bifrost_vm__)) {
                name = Bifrost.Guid.create();
                element.__bifrost_vm__ = name;
            } else {
                name = element.__bifrost_vm__;
                alreadySet = true;
            }

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
            if (!Bifrost.isNullOrUndefined(element.__bifrost_vm__)) {
                var name = element.__bifrost_vm__;
                self[name](null);
            }
        };
    })
});