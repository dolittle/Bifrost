describe("when getting stateful operations with one operation with state and one without", function () {
    var context = {
        someContext: 42
    };

    var state = {
        someState: 43
    };


    var operationWithState = {
        canPerform: ko.observable(true),
        perform: sinon.mock().returns(state)
    };

    var operationWithoutState = {
        canPerform: ko.observable(true),
        perform: sinon.mock().returns({})
    };

    var allOperations = [
        operationWithState,
        operationWithoutState
    ];

    var operationEntryFactory = {
        create: function (operation, state) {
            return {
                context: context,
                operation: operation,
                state: state
            }
        }
    };

    var operations = Bifrost.interaction.Operations.create({
        operationEntryFactory: operationEntryFactory
    });

    operations.perform(operationWithState);
    operations.perform(operationWithoutState);

    it("should perform the operation with state", function () {
        expect(operationWithState.perform.called).toBe(true);
    });

    it("should perform the operation without state", function () {
        expect(operationWithoutState.perform.called).toBe(true);
    });

    it("should add only one operation entry as stateful", function () {
        expect(operations.stateful().length).toBe(1);
    });

    it("should add the stateful operation entry as stateful", function () {
        expect(operations.stateful()[0].operation).toBe(operationWithState);
    });
});