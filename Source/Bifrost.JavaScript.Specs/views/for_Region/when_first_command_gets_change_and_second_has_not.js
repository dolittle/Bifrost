describe("when first command gets change and second has not", function () {

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
    var hasChanges = false;
    region.hasChanges.subscribe(function (newValue) {
        hasChanges = newValue;
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
    firstCommand.hasChanges(true);

    var secondCommand = {
        isValid: ko.observable(false),
        isAuthorized: ko.observable(false),
        canExecute: ko.observable(false),
        hasChanges: ko.observable(false),
        isReadyToExecute: ko.observable(false),
        validators: ko.observableArray()
    };

    region.commands.push(secondCommand);
    secondCommand.hasChanges(false);

    it("should have changes", function () {
        expect(hasChanges).toBe(true);
    });
});