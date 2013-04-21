describe("when applying validation results to properties", function () {
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
        this.message = ko.observable("");
        this.validate = sinon.stub();
    }

    command.something.validator = new validator();
    command.deep.property.validator = new validator();

    var parameters = {
        validationService: {
        }
    };

    var validationResults = [{
        errorMessage: "something is wrong",
        memberNames: ["something"]
    }, {
        errorMessage: "deep property is wrong",
        memberNames: ["deep.property"]
    }];

    var commandValidationService = Bifrost.commands.commandValidationService.create(parameters);
    commandValidationService.applyValidationResultToProperties(command, validationResults);

    it("should invalidate the top level property", function () {
        expect(command.something.validator.isValid()).toBe(false);
    });

    it("should add the validation message to the top level property", function () {
        expect(command.something.validator.message()).toBe(validationResults[0].errorMessage);
    });

    it("should invalidate the deep property", function () {
        expect(command.deep.property.validator.isValid()).toBe(false);
    });

    it("should add the validation message to the deep property", function () {
        expect(command.deep.property.validator.message()).toBe(validationResults[1].errorMessage);
    });

});