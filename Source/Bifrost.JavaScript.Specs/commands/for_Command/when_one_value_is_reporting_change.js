﻿describe("when one value is reporting change", function () {
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
        this.someValue = ko.observable(43);
        this.someArray = ko.observableArray();
    });

    var newValues = {
        someValue: 43,
        someArray: [1, 2, 3]
    };

    ko.extenders.hasChanges = function (target, options) {
        if (target() == 43) {
            target.hasChanges = ko.observable(true);
        } else {
            target.hasChanges = ko.observable(false);
        }
        target.setInitialValue = function () { }
    };


    var command = commandType.create(parameters);
    command.populatedExternally();
    command.populateFromExternalSource(newValues);

    it("should have changes", function () {
        expect(command.hasChanges()).toBe(true);
    });
});