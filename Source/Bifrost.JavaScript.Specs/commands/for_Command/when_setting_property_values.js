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
        },
        region: {
            commands: []
        },
        mapper: {
            mapToInstance: sinon.stub()
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

    var hasChangesExtender = ko.extenders.hasChanges;
    ko.extenders.hasChanges = function (target, options) {
        target.hasChanges = ko.observable();
        target.setInitialValue = sinon.stub()
    };

    var command = commandType.create(parameters);
    command.setPropertyValuesFrom(newValues);

    ko.extenders.hasChanges = hasChangesExtender;

    it("should forward to mapper", function () {
        expect(parameters.mapper.mapToInstance.calledWith(commandType, newValues, command)).toBe(true);
    });
});