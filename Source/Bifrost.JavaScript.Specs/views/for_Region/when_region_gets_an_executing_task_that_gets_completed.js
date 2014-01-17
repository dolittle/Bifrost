describe("when region gets an executing task that gets completed", function () {

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
                all: ko.observableArray()
            };
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

    var task = { _type: { typeOf: sinon.stub().returns(false) } };
    tasks.all.push(task);
    tasks.all.remove(task);

    it("should not be busy", function () {
        expect(region.isBusy()).toBe(false);
    });
});