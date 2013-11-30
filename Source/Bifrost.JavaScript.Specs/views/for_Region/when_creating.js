describe("when creating", function () {

    var messenger = {
        local:"messenger"
    };
    var globalMessenger = {
        global:"messenger"
    };
    var messengerFactory = {
        create: sinon.stub().returns(messenger),
        global: sinon.stub().returns(globalMessenger)
    };

    var operations = {
        some:"operations"
    };
    var operationsFactory = {
        create: sinon.stub().returns(operations)
    };

    var tasks = {
        all: ko.observableArray()
    };
    var tasksFactory = {
        create: sinon.stub().returns(tasks)
    };
    
    var instance = new Bifrost.views.Region(
        messengerFactory,
        operationsFactory,
        tasksFactory
    );

    it("should create a new messenger and put it on the region", function () {
        expect(instance.messenger).toBe(messenger);
    });

    it("should get the global messenger and put it on the region", function () {
        expect(instance.globalMessenger).toBe(globalMessenger);
    });

    it("should create operations and put it on the region", function () {
        expect(instance.operations).toBe(operations);
    });

    it("should create tasks and put it on the region", function () {
        expect(instance.tasks).toBe(tasks);
    });
});