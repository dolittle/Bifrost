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

    var server = sinon.fakeServer.create();

    server.respondWith("GET", "/Bifrost/CommandSecurity/GetForCommand?commandName=SomeCommand",
        [
            200,
            { "Content-Type": "application/json" },
            '{"isAuthorized": true}'
        ]);


    var service = Bifrost.commands.commandSecurityService.create(parameters);
    service.getContextFor({name:"SomeCommand"}).continueWith(function (context) {
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