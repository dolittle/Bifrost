describe("when there are no commands", function () {

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

    it("should make the region valid", function () {
        expect(region.isValid()).toBe(true);
    });

    it("should not be able to execute commands", function () {
        expect(region.canCommandsExecute()).toBe(false);
    });

    it("should not have changes for commands", function () {
        expect(region.hasChanges()).toBe(false);
    });

    it("should not consider commands ready to execute", function () {
        expect(region.areCommandsReadyToExecute()).toBe(false);
    });
});