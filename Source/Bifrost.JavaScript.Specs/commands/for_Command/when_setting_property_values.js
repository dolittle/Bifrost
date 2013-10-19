describe("when setting property values", function () {
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

    ko.extenders.hasChanges = function (target, options) {
        target.hasChanges = ko.observable();
        target.setInitialValue = sinon.stub()
    };

    var command = commandType.create(parameters);
    command.setPropertyValuesFrom(newValues);

    it("should update the observable", function () {
        expect(command.someValue()).toBe(newValues.someValue);
    });

    it("should update the observable array", function () {
        expect(command.someArray()).toBe(newValues.someArray);
    });

    it("should set the observable value for the has changes extension", function () {
        expect(command.someValue.setInitialValue.called).toBe(true);
    });

    it("should set the observable array value for the has changes extension", function () {
        expect(command.someArray.setInitialValue.called).toBe(true);
    });
});