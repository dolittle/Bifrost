describe("when matching with property filters", function () {
    var propertyFilters = { some: "filter" };
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

    readModelOf.instanceMatching(propertyFilters);

    it("should create a read model task", function () {
        expect(taskFactory.createReadModel.calledWith(readModelOf, propertyFilters)).toBe(true);
    });

    it("should execute task", function () {
        expect(region.tasks.execute.calledWith(task)).toBe(true);
    });

    it("should map resulting data to read model", function () {
        expect(mapper.map.calledWith(readModelOf._readModelType, data)).toBe(true);
    });

    it("should set the instance to the result coming back", function () {
        expect(readModelOf.instance()).toBe(readModel);
    });
});