describe("when performing", function () {
    var context = {
        someContext: 42
    };

    var state = {
        someState: 43
    };

    var operation = {
        canPerform: ko.observable(true),
        perform: sinon.mock().withArgs(context).returns(state)
    };

    var entry = {
        context: context,
        operation: operation,
        state: state
    };

    var operationEntryFactory = {
        create: sinon.mock().withArgs(context, operation, state).returns(entry)
    };

    var operations = Bifrost.interaction.Operations.create({
        operationEntryFactory : operationEntryFactory
    });

    operations.perform(context, operation);

    it("should perform the operation", function () {
        expect(operation.perform.called).toBe(true);
    });

    it("should create an operation entry", function () {
        expect(operationEntryFactory.create.called).toBe(true);
    });

    it("should add the operation entry", function () {
        expect(operations.all()[0]).toBe(entry);
    });
});