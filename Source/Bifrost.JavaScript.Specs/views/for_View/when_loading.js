describe("when loading", function () {
    var html = "<div>some content</div>";
    var pathLoaded = null;

    var options = {
        viewLoader: {
            load: function (path) {
                pathLoaded = path;
                var promise = Bifrost.execution.Promise.create();
                promise.signal(html);
                return promise;
            }
        },
        viewModelManager: {
            hasForView: function () { return false; }
        },
        viewManager: {
            expandFor: sinon.stub()
        }
    };
    var view = Bifrost.views.View.create(options);

    view.load("somePath");

    it("should forward loading to the view loader", function () {
        expect(pathLoaded).toBe("somePath");
    });

    it("should set path on the view", function () {
        expect(view.path).toBe("somePath");
    });

    it("should set the content coming from the load", function () {
        expect(view.content).toBe(html);
    });

    it("should recursively expand any views", function () {
        expect(options.viewManager.expandFor.called).toBe(true);
    });
});