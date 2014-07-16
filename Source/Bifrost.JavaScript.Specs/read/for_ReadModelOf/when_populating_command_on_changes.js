describe("when populating command on changes", function () {

    var readModelOf = Bifrost.read.ReadModelOf.create({
        mapper: {},
        region: {},
        taskFactory: {},
        readModelSystemEvents: {}
    });

    var command = {
        populatedExternally: sinon.stub(),
        populateFromExternalSource: sinon.stub()
    };

    readModelOf.populateCommandOnChanges(command);

    it("should tell command that it will be populated", function () {
        expect(command.populatedExternally.called).toBe(true);
    });

    it("should populate the command with the default instance", function () {
        expect(command.populateFromExternalSource.calledWith(readModelOf.instance())).toBe(true);
    });
});