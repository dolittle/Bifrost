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

    var childRegion = {
        isLoading: ko.observable(false),
        isBusy: ko.observable(false),
        isValid: ko.observable(false),
        isComposing: ko.observable(false),
        isExecuting: ko.observable(false),
        validationMessages: ko.observableArray(),
        aggregatedCommands: ko.observableArray(),
        canCommandsExecute: ko.observable(false),
        areCommandsAuthorized: ko.observable(false),
        commandsHaveChanges: ko.observable(false),
        areCommandsReadyToExecute: ko.observable(false),
        hasChanges: ko.observable(false)
    };

    region.children.push(childRegion);
    childRegion.areCommandsReadyToExecute(true);
    childRegion.areCommandsReadyToExecute(false);

    it("should not be ready to execute", function () {
        expect(isReadyToExecute).toBe(false);
    });
});