describe("when creating command of type with after command created callback", function () {
    var commandInstance = {myCommand:42};
    var commandType = {
        create: sinon.stub().returns(commandInstance),
        some: "commandType"
    };

    var securityContext = {};
    var commandSecurityService = {};
    var region = {};

    var context = {};

    var instance = Bifrost.interaction.CommandOperation.create({
        commandSecurityService: commandSecurityService,
        region: region,
        context: context
    });

    instance.afterCommandCreated = sinon.stub();

    result = instance.createCommandOfType(commandType)

    it("should call the callback", function () {
        expect(instance.afterCommandCreated.calledWith(commandInstance)).toBe(true);
    });
});