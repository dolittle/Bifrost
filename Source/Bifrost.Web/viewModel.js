(function (undefined) {
    function ViewModel() {
        var self = this;

        this.message = ko.observable();

        this.stringParameter = ko.observable();

        this.doStuffCommand = Bifrost.commands.Command.create({
            name: 'DoStuffCommand',
            context: self,
            error: function (commandResult) {
                self.message(commandResult.Exception.Message);
            },
            success: function (commandResult) {
                self.message("We got it");
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

        this.doOtherStuffCommand = Bifrost.commands.Command.create({
            name: 'DoOtherStuffCommand',
            context: self
        });

        this.doItAll = function () {
            var saga = Bifrost.sagas.Saga.create({ Id: Bifrost.Guid.create() });
            Bifrost.commands.commandCoordinator.handleForSaga(saga, [self.doStuffCommand, self.doOtherStuffCommand]);
        };
    }

    $(function () {
        var vm = new ViewModel();
        ko.applyBindings(vm);
        window.vm = vm;
    });
})();