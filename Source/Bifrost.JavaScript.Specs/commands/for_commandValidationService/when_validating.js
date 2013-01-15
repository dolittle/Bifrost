describe("when validating", function () {
    var command = {
        name: "something",
        something: ko.observable(),
        deep: {
            property: ko.observable()
        }
    };

    function validator() {
        var self = this;

        this.validate = sinon.stub();
    }

    command.something.validator = new validator();
    command.deep.property.validator = new validator();

    var parameters = {
        validationService: {
        }
    };

    var commandValidationService = Bifrost.commands.commandValidationService.create(parameters);
    commandValidationService.validate(command);

    it("should call validate on the top level property", function () {
        expect(command.something.validator.validate.called).toBe(true);
    });

    it("should call validate on the deep level property", function () {
        expect(command.deep.property.validator.validate.called).toBe(true);
    });
});