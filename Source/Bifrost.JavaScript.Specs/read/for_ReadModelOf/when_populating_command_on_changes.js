describe("when populating command on changes", function () {

    var readModelOf = Bifrost.read.ReadModelOf.create({
        readModelMapper: {}
    });

    var command = {
        populatedExternally: sinon.stub()
    };

    readModelOf.populateCommandOnChanges(command);

    it("should tell command that it will be populated", function () {
        expect(command.populatedExternally.called).toBe(true);
    });
});