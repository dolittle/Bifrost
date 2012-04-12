﻿describe("when creating a list of parameters", function () {
    var command = {
        name: "DoSomething",
        id: Bifrost.Guid.create(),
        parameters: {
            plainString: "test",
            koString: ko.observable("test")
        }
    };
    var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);


    it("should include the plainString", function () {
        expect(JSON.parse(commandDescriptor.Command).plainString).toBeDefined();
    });

    it("should include the koString", function () {
        expect(JSON.parse(commandDescriptor.Command).koString).toBeDefined();
    });
});