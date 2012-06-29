describe("when applying the same command multiple times", sinon.test(function () {
    var expectedMessage = "Should be required",
        test = {},
        server,
        command1;


    beforeEach(function () {
        server = sinon.stub($, "ajax");


        command1 = {
            name: "Whatevva",
            validatorsList: [],
            parameters: {
                something: ko.observable()
            }
        };
        command2 = {
            name: "Whatevva",
            validatorsList: [],
            parameters: {
                something: ko.observable()
            }
        };

        Bifrost.validation.validationService.resetCache();
        Bifrost.validation.validationService.applyForCommand(command1);
        Bifrost.validation.validationService.applyForCommand(command2);

    });
    afterEach(function () {
        server.restore();
    });

    it("should only contact the server once", function () {
        expect(server.callCount).toBe(1);
    });
}));