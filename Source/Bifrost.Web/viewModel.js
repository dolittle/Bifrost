(function (undefined) {
    function ViewModel() {
        var self = this;

        this.message = ko.observable();
        this.persistedStuff = ko.observableArray();

        this.doStuffCommand = Bifrost.commands.Command.create({
            name: 'DoStuffCommand',
            context: self,
            error: function (commandResult) {
                self.message(commandResult.exception.message);
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
                stringParameter: ko.observable(""),
                intParameter: ko.observable()
            }
        });

        this.loadPersistedStuff = function () {

            $.getJSON("/StuffToPersist/GetAll", function (d) {
                var mapped = ko.mapping.fromJS(d);

                $.each(mapped(), function (index, e) {
                    var found = false;
                    $.each(self.persistedStuff(), function (i, ee) {
                        if (ee.id() == e.id()) {
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