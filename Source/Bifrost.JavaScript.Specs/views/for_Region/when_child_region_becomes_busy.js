describe("when child region becomes busy", function () {

    var tasks = {
        all: ko.observableArray()
    };

    var messengerFactory = {
        create: function () { },
        global: function () { }
    };
    var operationsFactory = {
        create: function () { }
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

    var childRegion = {
        isLoading: ko.observable(false),
        isBusy: ko.observable(false),
        isValid: ko.observable(false),
        isComposing: ko.observable(false),
        isExecuting: ko.observable(false),
        validationMessages: ko.observableArray()
    };

    region.children.push(childRegion);

    childRegion.isBusy(true);

    it("should become busy", function () {
        expect(region.isBusy()).toBe(true);
    });
});