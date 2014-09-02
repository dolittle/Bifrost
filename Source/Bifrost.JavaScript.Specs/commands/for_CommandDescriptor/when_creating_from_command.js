describe("when creating from command", function () {
    var command = {
        _name: "DoSomething",
        _generatedFrom: "Somewhere",
        id: Bifrost.Guid.create(),
        parameters: {},
        someFunction: function () { },
        someObservable: ko.observable(1)
    };
    var commandDescriptor = null;

    var oldCommand = Bifrost.commands.Command;

    beforeEach(function () {

        Bifrost.commands.Command = {
            create: function () {
                return {
                    _name: "",
                    id: ""
                };
            }
        };

        commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);

    });

    afterEach(function () {
        Bifrost.commands.Command = oldCommand;
    });

    it("should return an instance", function () {
        expect(commandDescriptor).toBeDefined();
    });

    it("should set name", function () {
        expect(commandDescriptor.name).toEqual(command._name);
    });

    it("should set generated from", function () {
        expect(commandDescriptor.generatedFrom).toEqual(command._generatedFrom);
    });

    it("should include a command property", function () {
        expect(commandDescriptor.command).toBeDefined();
    });

    it("should not have the name on the command property", function () {
        expect(JSON.parse(commandDescriptor.command).name).toBeUndefined();
    });

    it("should not have the id on the command property", function () {
        expect(JSON.parse(commandDescriptor.command).id).toBeUndefined();
    });

    it("should not have the function on the command property", function () {
        expect(JSON.parse(commandDescriptor.command).someFunction).toBeUndefined();
    });

    it("should have the observable on the command property", function () {
        expect(JSON.parse(commandDescriptor.command).someObservable).toBeDefined();
    });
});