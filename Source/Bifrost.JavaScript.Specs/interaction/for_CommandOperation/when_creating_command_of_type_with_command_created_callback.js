describe("when creating command of type with command created callback", function () {
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

    var commandCreated = sinon.stub();

    instance.commandCreated.subscribe(commandCreated);

    result = instance.createCommandOfType(commandType)

    it("should call the callback", function () {
        expect(commandCreated.calledWith(commandInstance)).toBe(true);
    });
});