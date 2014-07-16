describe("when populating a command on changes and an instance exists", function () {
    var readModelOf = Bifrost.read.ReadModelOf.create({
        mapper: {},
        region: {},
        taskFactory: {},
        readModelSystemEvents: {}
    });

    var newInstance = { something : 42 };

    var command = {
        populatedExternally: function () { },
        populateFromExternalSource: sinon.mock().withArgs(newInstance)
    };

    readModelOf.instance(newInstance);
    readModelOf.populateCommandOnChanges(command);

    it("should initialize command with the instance", function () {
        expect(command.populateFromExternalSource.called).toBe(true);
    });
});