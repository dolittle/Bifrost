describe("when command in child region can execute and then not", function () {

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
    var canExecute = false;
    region.canCommandsExecute.subscribe(function (newValue) {
        canExecute = newValue;
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
        isReadyToExecute: ko.observable(false),
        validators: ko.observableArray()
    };

    region.commands.push(command);

    command.canExecute(false);

    it("should not be able to execute", function () {
        expect(canExecute).toBe(false);
    });
});