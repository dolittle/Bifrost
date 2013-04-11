describe("when assets manager is initialized", function () {
    var readyCalled = false;
    var assetsManagerPromise = Bifrost.execution.Promise.create();

    beforeEach(function () {

        Bifrost.assetsManager = {
            initialize: function () {
                return assetsManagerPromise;
            }
        };
        
        Bifrost.namespace("Bifrost.navigation", {
            navigationManager: {
                hookup: function () { }
            }
        });
        Bifrost.namespace("Bifrost.features", {
            featureManager: {
                hookup: function () { }
            }
        });

        Bifrost.namespace("Bifrost", {
            StringMapper: {
                create: function() {
                    return {
                        addMapping: function() {}
                    }
                }
            },
            uriMappers: {},
            namespaceMappers: {}
        });
        Bifrost.WellKnownTypesDependencyResolver = {
            types: {}
        };
        

        Bifrost.configure.ready(function () {
            readyCalled = true;
        });

        Bifrost.configure.onStartup();

        assetsManagerPromise.signal();
    });

    afterEach(function () {
        Bifrost.configure.reset();
    });

    it("should call the ready callback", function () {
        expect(readyCalled).toBe(true);
    });

});