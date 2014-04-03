describe("when adding operation without state", function () {

    var tasks = {
        all: ko.observableArray()
    };

    var operations = {
        all: ko.observableArray(),
        stateful: ko.observableArray()
    };

    var messengerFactory = {
        create: function () { },
        global: function () { }
    };
    var operationsFactory = {
        create: function () { return operations; }
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

    operations.all.push({});

    it("should not have changes", function () {
        expect(hasChanges).toBe(false);
    });
});