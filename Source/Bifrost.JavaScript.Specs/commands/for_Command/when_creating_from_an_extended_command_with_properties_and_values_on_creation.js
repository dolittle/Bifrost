describe("when creating from an observable extended command with properties and values on creation", function () {
    var commandAppliedTo = null;
    var command = null;

    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
                commandAppliedTo = command
            },
            validateSilently: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        options: {
            properties: {
                integer: 5,
                number: 5.3,
                string: "hello",
                arrayOfIntegers: [1, 2, 3]
            }
        },
        region: {
            commands: []
        },
        mapper: {}
    }

    var commandType = Bifrost.commands.Command.extend(function () {
        this.integer = 0;
        this.number = 0.1;
        this.string = "";
        this.arrayOfIntegers = [];

        this.onCreated = function () {
        };

        this.onDispose = function () {
        };
    });

    command = commandType.create(parameters);

    it("should make the integer property as an observable", function () {
        expect(ko.isObservable(command.integer)).toBe(true);
    });

    it("should make the number property as an observable", function () {
        expect(ko.isObservable(command.number)).toBe(true);
    });

    it("should make the string property as an observable", function () {
        expect(ko.isObservable(command.string)).toBe(true);
    });

    it("should make the array property as an observable", function () {
        expect(ko.isObservable(command.arrayOfIntegers)).toBe(true);
    });

    it("should initialize the integer", function () {
        expect(command.integer()).toBe(parameters.options.properties.integer);
    });

    it("should initialize the number", function () {
        expect(command.number()).toBe(parameters.options.properties.number);
    });

    it("should initialize the string", function () {
        expect(command.string()).toBe(parameters.options.properties.string);
    });

    it("should initialize the array", function () {
        expect(command.arrayOfIntegers()).toBe(parameters.options.properties.arrayOfIntegers);
    });

});