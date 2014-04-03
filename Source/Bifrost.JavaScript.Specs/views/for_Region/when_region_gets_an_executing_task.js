describe("when region gets an executing task", function () {

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

    tasks.all.push({ _type : { typeOf : sinon.stub().returns(false)} });

    it("should become busy", function () {
        expect(region.isBusy()).toBe(true);
    });
});