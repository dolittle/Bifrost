describe("when creating a list of parameters", function () {
    var command = {
        name: "DoSomething",
        id: Bifrost.Guid.create(),
        parameters: {
            plainObject: {
                plainString: "test",
                koString: ko.observable("test"),
                koComputed: ko.computed(function () {
                    return "test";
                })
            },
            koObject: ko.observable({
                plainString: "test",
                koString: ko.observable("test"),
                koComputed: ko.computed(function () {
                    return "test";
                })
            })
        }
    };
    var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);


    it("should include the plainObject.plainString", function () {
        expect(JSON.parse(commandDescriptor.Command).plainObject.plainString).toBeDefined();
    });

    it("should include the plainObject.koString", function () {
        expect(JSON.parse(commandDescriptor.Command).plainObject.koString).toBeDefined();
    });

    it("should include the plainObject.koComputed", function () {
        expect(JSON.parse(commandDescriptor.Command).plainObject.koComputed).toBeDefined();
    });

    it("should include the koObject.koString", function () {
        expect(JSON.parse(commandDescriptor.Command).koObject.koString).toBeDefined();
    });

    it("should include the koObject.plainString", function () {
        expect(JSON.parse(commandDescriptor.Command).koObject.plainString).toBeDefined();
    });

    it("should include the koObject.koComputed", function () {
        expect(JSON.parse(commandDescriptor.Command).koObject.koComputed).toBeDefined();
    });
});