(function (undefined) {
    function ViewModel() {
        self = this;

        this.message = ko.observable();
        this.stringParameter = ko.observable();


        this.doStuffCommand = Bifrost.commands.create({
            name: 'DoStuffCommand',
            context: self,
            onSuccess: function (command) {
                command.context.message("We got it");
            },

            beforeExecute: function (command) {
                var stringParameter = command.parameters.stringParameter();
                if (stringParameter == "" ||
                     stringParameter == undefined) {
                    command.canExecute(false);
                } else {
                    command.canExecute(true);
                }
            },
            parameters: {
                stringParameter: ko.dependentObservable(self.stringParameter),
                intParameter: ko.observable()
            }
        });

        this.doOtherStuffCommand = Bifrost.commands.create({
            name: 'DoOtherStuffCommand',
            context: self
        });
    }

    $(function () {
        var vm = new ViewModel();
        ko.applyBindings(vm);
        window.vm = vm;
    });
})();