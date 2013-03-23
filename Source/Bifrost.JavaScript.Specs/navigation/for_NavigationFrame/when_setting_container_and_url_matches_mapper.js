describe("when setting container and url matches mapper", function () {
    var container = $("<div/>")[0];
    var createFromPath = null;
    var view = {
        content : "<div>Hello world</div>"
    };

    var stringMapper = {
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
    var viewFactory = {
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
        stringMapper: stringMapper,
        home: home,
        history: history,
        viewFactory: viewFactory
    });

    frame.setContainer(container);

    it("should create a view from the path", function () {
        expect(createFromPath).toBe("ThePath");
    });

    it("should set current view when view is loaded", function () {
        expect(frame.currentView()).toBe(view);
    });

    it("should set the content of the container with the view", function () {
        expect($(container).html()).toBe(view.content);
    });
});