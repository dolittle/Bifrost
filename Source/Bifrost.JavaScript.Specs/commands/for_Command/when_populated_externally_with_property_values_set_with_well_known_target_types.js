describe("when populated externally with property values set with well known target types", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
            },
            validateSilently: sinon.stub(),
            clearValidationMessagesFor: sinon.stub()
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
        typeConverters: {
            convertFrom: sinon.stub().returns(43)
        }
    }

    var commandType = Bifrost.commands.Command.extend(function () {
        this.someValue = ko.observable(42);

        this.someValue._typeAsString = "Number";
    });

    

    var newValues = {
        someValue: "43",
    };

    var command = commandType.create(parameters);
    command.populatedExternally();
    command.populateFromExternalSource(newValues);

    it("should call the type converter with the string value and the target type", function () {
        expect(parameters.typeConverters.convertFrom.calledWith("43", "Number")).toBe(true);
    });

    it("should set the converted value", function () {
        expect(command.someValue()).toBe(43);
    });
});