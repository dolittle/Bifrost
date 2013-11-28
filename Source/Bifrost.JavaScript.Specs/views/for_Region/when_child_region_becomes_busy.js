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
        isBusy: ko.observable(false)
    };

    region.children.push(childRegion);

    childRegion.isBusy(true);

    it("should become busy", function () {
        expect(region.isBusy()).toBe(true);
    });
});