describe("when populating from external source with property values", function () {
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
    command.populateFromExternalSource(newValues);

    it("should be considered populated externally", function () {
        expect(command.isPopulatedExternally()).toBe(true);
    });

    it("should be considered ready", function () {
        expect(command.isReady()).toBe(true);
    });
});