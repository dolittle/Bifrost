describe("when getting context for a command", function () {
    var securityContext = "SecurityContext";
    var securityContextReceived = null;
    var createCalled = false;

    var parameters = {
        commandSecurityContextFactory: {
            create: function () {
                createCalled = true;
                return securityContext;
            }
        }
    };

    var service = Bifrost.commands.commandSecurityService.create(parameters);
    service.getContextFor({}).continueWith(function (context) {
        securityContextReceived = context;
    });

    it("should create a security context", function () {
        expect(createCalled).toBe(true);
    });
    
    it("should continue the promise with the security context", function () {
        expect(securityContextReceived).toBe(securityContext);
    });
});