describe("when setting property values", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
            }
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        options: {
        }
    }

    var commandType = Bifrost.commands.Command.extend(function () {
        this.someValue = ko.observable(42);
        this.someArray = ko.observableArray();
    });

    var newValues = {
        someValue: 43,
        someArray: [1, 2, 3]
    };

    var command = commandType.create(parameters);
    command.setPropertyValuesFrom(newValues);

    it("should update the observable", function () {
        expect(command.someValue()).toBe(newValues.someValue);
    });

    it("should update the observable array", function () {
        expect(command.someArray()).toBe(newValues.someArray);
    });
});