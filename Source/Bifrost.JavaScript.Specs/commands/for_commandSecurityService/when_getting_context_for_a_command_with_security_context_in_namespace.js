describe("when getting context for a command with security context in namespace", sinon.test(function () {
    var securityContext = {
        isAuthorized: ko.observable(false)
    };
    var securityContextReceived = null;
    var createCalled = false;

    var parameters = {
        commandSecurityContextFactory: {
            create: sinon.stub()
        }
    };

    var command = {
        name: "SomeCommand",
        generatedFrom : "SomeCommand",
        _type: {
            _name: "SomeCommand",
            _namespace: {
                SomeCommandSecurityContext: securityContext
            }
        }
    };
    var service = Bifrost.commands.commandSecurityService.create(parameters);
    service.getContextFor(command).continueWith(function (context) {
        securityContextReceived = context;
    });


    it("should not create a security context", function () {
        expect(parameters.commandSecurityContextFactory.create.called).toBe(false);
    });
    
    it("should continue the promise with the security context found in namespace", function () {
        expect(securityContextReceived).toBe(securityContext);
    });
}));