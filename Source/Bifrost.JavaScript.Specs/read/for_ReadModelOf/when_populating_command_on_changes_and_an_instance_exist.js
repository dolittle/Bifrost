describe("when populating a command on changes and an instance exists", function () {
    var readModelOf = Bifrost.read.ReadModelOf.create({
        readModelMapper: {}
    });

    var newInstance = { something : 42 };

    var command = {
        populatedExternally: function () { },
        setPropertyValuesFrom: sinon.mock().withArgs(newInstance)
    };

    readModelOf.instance(newInstance);
    readModelOf.populateCommandOnChanges(command);

    it("should initialize command with the instance", function () {
        expect(command.setPropertyValuesFrom.called).toBe(true);
    });
});