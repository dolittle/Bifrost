describe("when validating with valid properties", function () {
    var command = {
        name: "something",
        something: ko.observable(),
        deep: {
            property: ko.observable()
        }
    };

    function validator() {
        var self = this;

        this.isValid = ko.observable(true);

        this.validate = sinon.stub();
    }

    command.something.validator = new validator();
    command.deep.property.validator = new validator();

    var parameters = {
        validationService: {
        }
    };

    var commandValidationService = Bifrost.commands.commandValidationService.create(parameters);
    var result = commandValidationService.validate(command);

    it("should return a result with valid set to true", function () {
        expect(result.valid).toBe(true);
    });
});