describe("when instance changes and command is linked", function () {
    var readModelOf = Bifrost.read.ReadModelOf.create({
        readModelMapper: {},
        region: {},
        taskFactory: {},
        readModelSystemEvents: {}
    });

    var newInstance = { something : 42 };

    var command = {
        populatedExternally: function () { },
        populateFromExternalSource: sinon.stub()
    };

    readModelOf.populateCommandOnChanges(command);
    readModelOf.instance(newInstance);

    it("should initialize command with the instance", function () {
        expect(command.populateFromExternalSource.calledWith(newInstance)).toBe(true);
    });
});