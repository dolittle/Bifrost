describe("when command in child region is ready to execute and then not", function () {

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

    var childRegion = new Bifrost.views.Region(
        messengerFactory,
        operationsFactory,
        tasksFactory
    );
    region.children.push(childRegion);

    var command = {
        isValid: ko.observable(false),
        isAuthorized: ko.observable(false),
        canExecute: ko.observable(true),
        hasChanges: ko.observable(false),
        isReadyToExecute: ko.observable(true),
        validators: ko.observableArray()
    };

    region.commands.push(command);

    command.isReadyToExecute(false);

    it("should not be ready to execute", function () {
        expect(isReadyToExecute).toBe(false);
    });
});