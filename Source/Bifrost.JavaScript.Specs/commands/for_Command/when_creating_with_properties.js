describe("when creating with properties", function () {
    var commandAppliedTo = null;
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
                commandAppliedTo = command
            }
        },
        options: {
            properties: {
                integer: 5,
                number: 5.3,
                string: "hello",
                arrayOfIntegers: [1, 2, 3]
            },
            name:"something"
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should add the integer property as an observable", function () {
        expect(ko.isObservable(command.integer)).toBe(true);
    });

    it("should add the number property as an observable", function () {
        expect(ko.isObservable(command.number)).toBe(true);
    });

    it("should add the string property as an observable", function () {
        expect(ko.isObservable(command.string)).toBe(true);
    });

    it("should add the array property as an observable", function () {
        expect(ko.isObservable(command.arrayOfIntegers)).toBe(true);
    });

    it("should set the integer value from properties", function () {
        expect(command.integer()).toBe(parameters.options.properties.integer);
    });

    it("should set the number value from properties", function () {
        expect(command.number()).toBe(parameters.options.properties.number);
    });

    it("should set the string value from properties", function () {
        expect(command.string()).toBe(parameters.options.properties.string);
    });

    it("should set the string value from properties", function () {
        expect(command.arrayOfIntegers()).toBe(parameters.options.properties.arrayOfIntegers);
    });

    it("should apply validation rules to properties", function () {
        expect(commandAppliedTo).toBe(command);
    });
});