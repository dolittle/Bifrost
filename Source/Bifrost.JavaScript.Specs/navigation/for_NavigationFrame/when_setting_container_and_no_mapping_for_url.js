describe("when setting container and no mapping for url", function () {
    var container = $("<div/>")[0];
    var uriMapper = {
        resolve: function (input) {
            if (input == "Something") return "ThePath";
        },
        hasMappingFor: function () {
            return false;
        }
    };
    var home = "The Home";
    var history = {
        Adapter: {
            bind: function () { }
        },
        getState: function() {
            return {
                url:"http://localhost/Something"
            }
        }
    };

    var frame = Bifrost.navigation.NavigationFrame.create({
        uriMapper: uriMapper,
        home: home,
        history: history
    });

    frame.configureFor(container);

    it("should set the current uri to home", function () {
        expect(frame.currentUri()).toBe(home);
    });
});