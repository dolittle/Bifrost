describe("when loading and instance is null", function () {
    var propertyFilters = { some: "filter" };
    var task = { some: "task" };
    var taskFactory = {
        createReadModel: sinon.stub().returns(task)
    };
    var readModel = {
        some: "readModel"
    };
    var region = {
        tasks: {
            execute: sinon.stub().returns({
                continueWith: function (callback) {
                    callback(null);
                }
            })
        }
    };

    var mapper = {
        map: sinon.stub()
    };

    var readModelSystemEvents = {
        noInstance: {
            trigger: sinon.stub()
        }
    };

    var readModelOf = Bifrost.read.ReadModelOf.create({
        mapper: mapper,
        region: region,
        taskFactory: taskFactory,
        readModelSystemEvents: readModelSystemEvents
    });

    readModelOf.instanceMatching(propertyFilters);

    it("should not map resulting data to read model", function () {
        expect(mapper.map.called).toBe(false);
    });

    it("should trigger the no instance system event", function () {
        expect(readModelSystemEvents.noInstance.trigger.calledWith(readModelOf)).toBe(true);
    });
});