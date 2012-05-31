describe("when validating the command", function () {
    var options,
        expectedMessage,
        server,
        validations,
        command;

    beforeEach(function () {
        options = {
            error: function () {
                print("Error");
            },
            success: function () {
            },
            parameters: {
                plainValue: "test",
                computed: ko.computed(function () { return "test"; }),
                plainObject: {
                    plainValue: "test",
                    observable: ko.observable("test")
                }
            }
        };
        expectedMessage = "it should be here";
        server = sinon.fakeServer.create();

        server.respondWith("POST", "/Validation/GetForCommand",
            [200, { "Content-Type": "application/json" }, ko.toJSON({
                properties: {
                    "computed": {
                        required: {
                            message: expectedMessage
                        }
                    },
                    "plainObject.observable": {
                        required: {
                            message: expectedMessage
                        }
                    }
                }
            })]
        );
        validations = 0;

        function validate() {
            validations++;
        }

        command = Bifrost.commands.Command.create(options);
        server.respond();
        options.parameters.computed.validator = { validate: validate };
        options.parameters.plainObject.observable.validator = { validate: validate };
        command.validate();
    });

    afterEach(function () {
        server.restore();
    })

    it("should call validate for each object", function () {
        expect(validations).toBe(2);
    });
});