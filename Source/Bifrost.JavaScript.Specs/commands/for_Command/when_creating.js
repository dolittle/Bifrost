describe("when creating", function () {
    var commandAppliedTo = null;
    var commandAskedForSecurityContext = null;

    var securityContext = "SecurityContext";

    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
                commandAppliedTo = command
            }
        },
        commandSecurityService: {
            getContextFor: function (command) {
                commandAskedForSecurityContext = command;
                return {
                    continueWith: function (callback) {
                        callback(securityContext);
                    }
                }
            }
        },
        options: {
            name:"something"
        }
    }
    var command = null;

    ko.extenders.hasChanges = function (target, options) {
        print("JELLO : "+target);
    };

    command = Bifrost.commands.Command.create(parameters);

    it("should apply validation rules to properties", function () {
        expect(commandAppliedTo).toBe(command);
    });

    it("should get security context for command", function () {
        expect(commandAskedForSecurityContext).toBe(command);
    });

    it("should set the security context on the command", function () {
        expect(command.securityContext()).toBe(securityContext);
    });
});