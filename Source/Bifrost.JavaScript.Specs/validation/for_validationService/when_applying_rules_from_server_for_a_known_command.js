describe("when applying rules from server for a known command", sinon.test(function () {
    var expectedMessage = "Should be required";
    var test = this;

    var server = sinon.fakeServer.create();

    server.respondWith("POST", "/ValidationRules/GetForCommand",
        [200, { "Content-Type": "application/json" }, '{ "properties": { "something": { "required" : { "message" : "'+expectedMessage+'" } } } }']);

    var command = {
        name: "Whatevva",
        parameters: {
            something: {
                extend: function () {
                },
                validator: {
                    setOptions: function (options) {
                        test.optionsSet = options;
                    }
                }
            }
        }
    };

    Bifrost.validation.validationService.applyForCommand(command);
    server.respond();

    it("should set the rule in response from server", function () {
        expect(test.optionsSet.required).not.toBeUndefined();
    });
    it("should set the message for the rule in response from server", function () {
        expect(test.optionsSet.required.message).toBe(expectedMessage);
    });
}));