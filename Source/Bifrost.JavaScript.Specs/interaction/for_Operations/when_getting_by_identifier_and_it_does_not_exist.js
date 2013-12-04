describe("when getting by identifier and it does not exist", function () {
    var firstOperation = {
        identifier: "A03989FF-A2A1-40CE-94CE-EF5F34D1ADCF"
    };
    var secondOperation = {
        identifier: "7C9BB4F4-EFA7-43E6-8A39-F077DDD752C6"
    };


    var operations = Bifrost.interaction.Operations.create({
        operationEntryFactory: {}
    });

    operations.all.push(firstOperation);

    var operation = operations.getByIdentifier(secondOperation.identifier);

    it("should return null", function () {
        expect(operation).toBeNull();
    });
});