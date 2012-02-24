(function (undefined) {
    function ViewModel() {
        var self = this;

        this.message = ko.observable();
        this.persistedStuff = ko.observableArray();

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
                stringParameter: ko.observable(""),
                intParameter: ko.observable()
            }
        });

        ko.extenders.validation.extendAllProperties(this.doStuffCommand.parameters);

        var methodParameters = {
            name: "\"DoStuffCommand\""
        }
        $.ajax({
            type: "POST",
            url: "/ValidationRules/GetForCommand",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(methodParameters),
            complete: function (d) {
                var result = $.parseJSON($.parseJSON(d.responseText))
                for (var property in result) {
                    if (!self.doStuffCommand.parameters.hasOwnProperty(property)) {
                        self.doStuffCommand.parameters[property] = ko.observable();
                    }
                    self.doStuffCommand.parameters[property].validator.setOptions(result[property]);
                    /*extend({
                    validation: result[property]
                    });*/
                }
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