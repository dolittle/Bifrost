Bifrost.namespace("Bifrost.views", {
    MasterViewModel: Bifrost.Type.extend(function (documentService) {
        var self = this;

        function getNameFrom(viewModel) {
            var fullName = viewModel._type._namespace.name + "." + viewModel._type._name;
            return fullName;
        }


        this.set = function (viewModel, name) {
            if (self.hasOwnProperty(name)) {
                var existingViewModel = self[name]();
                if (!Bifrost.isNullOrUndefined(existingViewModel)) {
                    if (Bifrost.isFunction(existingViewModel.deactivated)) {
                        existingViewModel.deactivated();
                    }
                }

                console.log("Set viewModel second time");
                self[name](viewModel);
            } else {
                console.log("Set viewModel first time");
                self[name] = ko.observable(viewModel);
            }

            if (Bifrost.isFunction(viewModel.activated)) {
                viewModel.activated();
            }
        };

        this.clearFor = function (element) {
            if (!Bifrost.isNullOrUndefined(element.__bifrost_vm__)) {
                var name = element.__bifrost_vm__;
                self[name](null);
            }
        }

        this.apply = function () {

        }


        this.applyBindingExpressionForViewModel = function (element, viewModel) {
            var name = "";
            var alreadySet = false;
            if (Bifrost.isNullOrUndefined(element.__bifrost_vm__)) {
                name = Bifrost.Guid.create();
                element.__bifrost_vm__ = name;
            } else {
                name = element.__bifrost_vm__;
                alreadySet = true;
            }

            self.set(viewModel, name);
            documentService.setViewModelOn(element, viewModel);

            if (!alreadySet) {
                console.log("Set viewModel binding Expression - "+name);
                documentService.setViewModelBindingExpression(element, "$data['" + name + "']");
            }
        };

        this.applyBindingForViewModel = function (element, viewModel) {
            var name = "";
            var alreadySet = false;
            if (Bifrost.isNullOrUndefined(element.__bifrost_vm__)) {
                name = Bifrost.Guid.create();
                element.__bifrost_vm__ = name;
            } else {
                name = element.__bifrost_vm__;
                alreadySet = true;
            }

            //var name = getNameFrom(viewModel);
            self.set(viewModel, name);
            documentService.setViewModelOn(element, viewModel);

            if (!alreadySet) {
                console.log("Apply viewModel binding for - " + name);
                /*
                ko.applyBindingsToNode(element, {
                    'viewModel': self[name] //viewModel
                });*/
            }
        };
    })
});