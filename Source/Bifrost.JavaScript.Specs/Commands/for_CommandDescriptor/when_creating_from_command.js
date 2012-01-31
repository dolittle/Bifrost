describe("when creating from command", function () {
    var command = {
        name: "DoSomething",
        id: Bifrost.Guid.create(),
        parameters: {}
    };
    var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);

    it("should return an instance", function () {
        expect(commandDescriptor).toBeDefined();
    });

    it("should set name", function () {
        expect(commandDescriptor.Name).toEqual(command.name);
    });

    it("should include a command property", function () {
        expect(commandDescriptor.Command).toBeDefined();
    });
});