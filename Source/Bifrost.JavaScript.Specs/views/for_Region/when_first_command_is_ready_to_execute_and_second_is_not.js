describe("when first command is ready to execute and second is not", function () {

    var tasks = {
        all: ko.observableArray()
    };

    var messengerFactory = {
        create: function () { },
        global: function () { }
    };
    var operationsFactory = {
        create: function () { return { all: ko.observableArray() }}
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

    var firstCommand = {
        isValid: ko.observable(false),
        isAuthorized: ko.observable(false),
        canExecute: ko.observable(false),
        hasChanges: ko.observable(false),
        isReadyToExecute: ko.observable(false),
        validators: ko.observableArray()
    };
    region.commands.push(firstCommand);
    firstCommand.isReadyToExecute(true);

    var secondCommand = {
        isValid: ko.observable(false),
        isAuthorized: ko.observable(false),
        canExecute: ko.observable(false),
        hasChanges: ko.observable(false),
        isReadyToExecute: ko.observable(false),
        validators: ko.observableArray()
    };
    region.commands.push(secondCommand);
    secondCommand.isReadyToExecute(false);


    it("should not be ready to execute", function () {
        expect(isReadyToExecute).toBe(false);
    });
});