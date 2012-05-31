describe("when applying nested rules from server for a known command", sinon.test(function () {
    var expectedMessage = "Should be required",
        test,
        server,
        command;



    beforeEach(function () {
        test = {};
        server = sinon.fakeServer.create();

        server.respondWith("POST", "/Validation/GetForCommand",
        [200, { "Content-Type": "application/json" }, '{ "properties": { "something.someOtherThing": { "required" : { "message" : "' + expectedMessage + '" } } } }']);

        command = {
            name: "Whatevva",
            validatorsList: [],
            parameters: {
                something: {
                    someOtherThing: ko.observable()
                }
            }
        };

        Bifrost.validation.validationService.applyForCommand(command);
        command.parameters.something.someOtherThing.validator = {
            setOptions: function (options) {
                test.optionsSet = options;
            }
        };

        server.respond();
    });

    afterEach(function () {
        server.restore();
    });

    it("should set the rule in response from server", function () {
        expect(test.optionsSet.required).toBeDefined();
    });
    it("should set the message for the rule in response from server", function () {
        expect(test.optionsSet.required.message).toBe(expectedMessage);
    });
    it("should set the validatorsList on the command", function () {
        expect(command.validatorsList.length).toBe(1);
        expect(command.validatorsList[0]).toBe(command.parameters.something.someOtherThing);
    });
}));