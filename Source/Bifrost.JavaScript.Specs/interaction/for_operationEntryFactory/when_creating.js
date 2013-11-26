describe("when creating", function () {
    var existingOperationEntryType = null;

    var operationEntry = {
        something: 42
    };

    var operationContext = {
        someContext: 43
    };

    var operation = {
        someOperation: 44
    };

    var operationState = {
        someState: 45
    };

    var operationEntryType = {
        create: sinon.mock().withArgs({
            context: operationContext,
            operation: operation,
            state: operationState
        }).returns(operationEntry)
    };

    var instance = null;

    beforeEach(function () {
        existingOperationEntryType = Bifrost.interaction.OperationEntry;
        Bifrost.interaction.OperationEntry = operationEntryType;

        var factory = Bifrost.interaction.operationEntryFactory.create();
        instance = factory.create(operationContext, operation, operationState);
    });

    afterEach(function () {
        Bifrost.interaction.OperationEntry = existingOperationEntryType;
    });
    
    it("should create a new instance", function () {
        expect(operationEntryType.create.called).toBe(true);
    });

    it("should return the created instance", function () {
        expect(instance).toBe(operationEntry);
    });
});