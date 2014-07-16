describe("when matching with property filters with observables that change after initial setup", function () {
    var propertyFilters = { some: ko.observable("filter") };
    var task = { some: "task" };
    var taskFactory = {
        createReadModel: sinon.stub().returns(task)
    };
    var data = {
        some: "data"
    };
    var readModel = {
        some: "readModel"
    };
    var region = {
        tasks: {
            execute: sinon.stub().returns({
                continueWith: function (callback) {
                    callback(data);
                }
            })
        }
    };

    var mapper = {
        map: sinon.stub().returns(readModel)
    };

    var readModelOf = Bifrost.read.ReadModelOf.create({
        mapper: mapper,
        region: region,
        taskFactory: taskFactory,
        readModelSystemEvents: {}
    });
    readModelOf.instance = sinon.stub();

    readModelOf.instanceMatching(propertyFilters);

    propertyFilters.some("other filter");

    var expectedPropertyFilters = { some: "other filter" };

    it("should create a read model task", function () {
        expect(taskFactory.createReadModel.secondCall.calledWith(readModelOf, expectedPropertyFilters)).toBe(true);
    });
    
    it("should execute task", function () {
        expect(region.tasks.execute.secondCall.calledWith(task)).toBe(true);
    });

    it("should map resulting data to read model", function () {
        expect(mapper.map.secondCall.calledWith(readModelOf.readModelType, data)).toBe(true);
    });

    it("should set the instance to the result coming back", function () {
        expect(readModelOf.instance.secondCall.calledWith(readModel)).toBe(true);
    });
});