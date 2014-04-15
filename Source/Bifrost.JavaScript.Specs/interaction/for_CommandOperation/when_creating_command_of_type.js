describe("when creating command of type", function () {
    var commandInstance = { myCommand: 42 };
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

    var result = instance.createCommandOfType(commandType)

    it("should create an instance of the command", function () {
        expect(commandType.create.calledWith({ region: region })).toBe(true);
    });

    it("should return the command instance", function () {
        expect(result).toBe(commandInstance);
    });
});