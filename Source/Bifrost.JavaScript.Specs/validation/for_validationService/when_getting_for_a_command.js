describe("when getting for a command", sinon.test(function () {

    var expectedMessage = "Should be required";
    var test = this;
    var rulesReturned = null;

    var server = sinon.fakeServer.create();

    server.respondWith("GET", "/Validation/GetForCommand?name=SomeCommand",
        [200, { "Content-Type": "application/json" }, '{ "properties": { "something": { "required" : { "message" : "' + expectedMessage + '" } } } }']);

    var validationService = Bifrost.validation.validationService.create();
    validationService.getForCommand("SomeCommand").continueWith(function (rules) {
        rulesReturned = rules;
    });

    server.respond();

    it("should pass the rules along", function () {
        expect(rulesReturned.something.required).toBeDefined();
    });
}));