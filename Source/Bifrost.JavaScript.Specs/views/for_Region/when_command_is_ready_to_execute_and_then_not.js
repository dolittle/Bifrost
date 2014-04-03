describe("when command is ready to execute and then not", function () {

    var tasks = {
        all: ko.observableArray()
    };

    var messengerFactory = {
        create: function () { },
        global: function () { }
    };
    var operationsFactory = {
        create: function () {
            return {
                all: ko.observableArray(),
                stateful: ko.observableArray()
            }
        }
    };
    var tasksFactory = {
        create: function () {
            return tasks;
        }
    };

    var region = new Bifrost.views.Region(
        messengerFactory,
        operationsFactory,
        tasksFactory
    );
    var isReadyToExecute = false;
    region.areCommandsReadyToExecute.subscribe(function (newValue) {
        isReadyToExecute = newValue;
    });

    var command = {
        isValid: ko.observable(false),
        isAuthorized: ko.observable(false),
        canExecute: ko.observable(false),
        hasChanges: ko.observable(false),
        isReadyToExecute: ko.observable(false),
        validators: ko.observableArray()
    };

    region.commands.push(command);

    command.isReadyToExecute(true);
    command.isReadyToExecute(false);

    it("should be ready to execute", function () {
        expect(isReadyToExecute).toBe(false);
    });
});