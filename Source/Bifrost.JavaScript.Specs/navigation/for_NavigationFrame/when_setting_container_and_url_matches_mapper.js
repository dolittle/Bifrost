describe("when setting container and url matches mapper", function () {
    var container = $("<div/>")[0];
    var createFromPath = null;
    var element = $("<div/>");
    element.html("Hello world");

    var view = {
        element: element
    };

    var uriMapper = {
        resolve: function (input) {
            if (input == "Something") return "ThePath";
        }
    };
    var home = "";
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
    var viewManager = {
        createFrom: function (path) {
            createFromPath = path;
            return {
                continueWith: function (callback) {
                    callback(view);
                }
            }
        }
    };

    var frame = Bifrost.navigation.NavigationFrame.create({
        home: home,
        uriMapper: uriMapper,
        history: history,
        viewManager: viewManager
    });

    frame.setContainer(container);

    it("should create a view from the path", function () {
        expect(createFromPath).toBe("ThePath");
    });

    it("should set current view when view is loaded", function () {
        expect(frame.currentView()).toBe(view);
    });

    it("should set the content of the container with the view", function () {
        expect($(container).children(0).html()).toBe(view.element.html());
    });
});