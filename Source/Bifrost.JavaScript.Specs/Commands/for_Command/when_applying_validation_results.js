describe("when applying validation results", function () {

    var options,
        expectedMessage,
        server,
        command,
        validationMessage;

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

        command = Bifrost.commands.Command.create(options);
        server.respond();

        validationMessage = "message";

        command.applyValidationMessageToMembers(["computed", "plainObject.observable"], validationMessage);
    });

    afterEach(function() {
        server.restore();
    });


    it("should set the validation message on each member", function () {
        expect(options.parameters.computed.validator.message()).toBe(validationMessage);
    });
});