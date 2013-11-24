describe("when performing and operation can not perform", function () {
    var operation = {
        canPerform: ko.observable(false),
        perform: sinon.stub()
    };

    var operations = Bifrost.interaction.Operations.create({
        operationEntryFactory: {}
    });

    operations.perform({}, operation);

    it("should not perform the operation", function () {
        expect(operation.perform.called).toBe(false);
    });
});