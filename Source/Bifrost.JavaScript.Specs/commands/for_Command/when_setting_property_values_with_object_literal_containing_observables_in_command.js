describe("when setting property values with object literal containing observables in command", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
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
        },
        region: {
            commands: []
        },
        typeConverters: {}
    }

    var commandType = Bifrost.commands.Command.extend(function () {
        this.someValue = ko.observable(42);
        this.someArray = ko.observableArray();
        this.nested = {
            someValue: ko.observable(43),
            someArray: ko.observableArray()
        }
    });

    var newValues = {
        someValue: 43,
        someArray: [1, 2, 3],
        nested: {
            someValue: 44,
            someArray: [4,5,6]
        }
    };

    var command = commandType.create(parameters);
    command.setPropertyValuesFrom(newValues);

    it("should update the observable in the nested object", function () {
        expect(command.nested.someValue()).toBe(newValues.nested.someValue);
    });

    it("should update the observable array in the nested object", function () {
        expect(command.nested.someArray()).toBe(newValues.nested.someArray);
    });
});