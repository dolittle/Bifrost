describe("when validating", function () {
    var command = {
        name: "something",
        something: ko.observable("somethingObservable"),
        deep: {
            property: ko.observable("deepProperty")
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

    it("should return a result", function () {
        expect(result).toBeDefined();
    });


    it("should call validate on the top level property", function () {
        expect(command.something.validator.validate.called).toBe(true);
    });

    it("should call validate with the actual top level value", function () { 
        expect(command.something.validator.validate.calledWith("somethingObservable")).toBe(true);
    });

    it("should call validate on the deep level property", function () {
        expect(command.deep.property.validator.validate.called).toBe(true);
    });

    it("should call validate with the actual deep level value", function () {
        expect(command.deep.property.validator.validate.calledWith("deepProperty")).toBe(true);
    });
});