describe("when creating a list of parameters", function () {
    var command = {
        _name: "DoSomething",
        id: Bifrost.Guid.create(),
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
    };
    var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);

    
    it("should include the plainObject.plainString", function () {
        expect(JSON.parse(commandDescriptor.command).plainObject.plainString).toBeDefined();
    });

    it("should include the plainObject.koString", function () {
        expect(JSON.parse(commandDescriptor.command).plainObject.koString).toBeDefined();
    });
    
    it("should include the plainObject.koComputed", function () {
        expect(JSON.parse(commandDescriptor.command).plainObject.koComputed).toBeDefined();
    });

    it("should include the koObject.koString", function () {
        expect(JSON.parse(commandDescriptor.command).koObject.koString).toBeDefined();
    });

    it("should include the koObject.plainString", function () {
        expect(JSON.parse(commandDescriptor.command).koObject.plainString).toBeDefined();
    });

    it("should include the koObject.koComputed", function () {
        expect(JSON.parse(commandDescriptor.command).koObject.koComputed).toBeDefined();
    });
});