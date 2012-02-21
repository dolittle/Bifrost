(function (undefined) {
    function ViewModel() {
        var self = this;

        this.message = ko.observable();

        this.persistedStuff = ko.observableArray();

        this.stringParameter = ko.observable();

        this.doStuffCommand = Bifrost.commands.Command.create({
            name: 'DoStuffCommand',
            context: self,
            error: function (commandResult) {
                self.message(commandResult.Exception.Message);
            },
            success: function (commandResult) {
                self.message("We got it");
                self.loadPersistedStuff();
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


        this.loadPersistedStuff = function () {

            $.getJSON("/StuffToPersist/GetAll", function (d) {
                var mapped = ko.mapping.fromJS(d);

                $.each(mapped(), function (index, e) {
                    var found = false;
                    $.each(self.persistedStuff(), function (i, ee) {
                        if (ee.Id() == e.Id()) {
                            found = true;
                            return;
                        }
                    });

                    if (found == false) {
                        self.persistedStuff.push(e);
                    }
                });
            });
        };


        this.loadPersistedStuff();
    }

    $(function () {
        var vm = new ViewModel();
        ko.applyBindings(vm);
        window.vm = vm;
    });
})();