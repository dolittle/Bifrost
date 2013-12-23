describe("when command type gets set", function () {
    var commandType = { some: "commandType" };

    var securityContext = {
        isAuthorized: ko.observable("very much so")
    };

    var commandSecurityService = {
        getContextForType: sinon.stub().returns({
            continueWith: function (callback) {
                callback(securityContext);
            }
        })
    };

    var region = {};

    var context = {};

    var instance = Bifrost.interaction.CommandOperation.create({
        commandSecurityService: commandSecurityService,
        region: region,
        context: context
    });

    instance.commandType(commandType);

    it("should get the security context for the type", function () {
        expect(commandSecurityService.getContextForType.calledWith(commandType)).toBe(true);
    });

    it("should set can perform to what is in the security context", function () {
        expect(instance.canPerform()).toBe(securityContext.isAuthorized());
    });
});