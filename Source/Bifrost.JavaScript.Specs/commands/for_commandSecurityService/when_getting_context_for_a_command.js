describe("when getting context for a command", sinon.test(function () {
    var securityContext = {
        isAuthorized: ko.observable(false)
    };
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

    $.support.cors = true;

    var server = sinon.fakeServer.create();

    server.respondWith("GET", "/Bifrost/CommandSecurity/GetForCommand?commandName=SomeCommand",
        [
            200,
            { "Content-Type": "application/json" },
            '{"isAuthorized": true}'
        ]);

    var command = {
        _name: "SomeCommand",
        _generatedFrom : "SomeCommand",
        _type: {
            _name: "SomeCommand",
            _namespace: {}
        }
    };
    var service = Bifrost.commands.commandSecurityService.create(parameters);
    service.getContextFor(command).continueWith(function (context) {
        securityContextReceived = context;
    });

    server.respond();

    it("should create a security context", function () {
        expect(createCalled).toBe(true);
    });
    
    it("should continue the promise with the security context", function () {
        expect(securityContextReceived).toBe(securityContext);
    });

    it("should be authorized", function () {
        expect(securityContext.isAuthorized()).toBe(true);
    });
}));