describe("when loading", function () {
    var html = "<div>some content</div>";
    var pathLoaded = null;
    var continuedWithView = null;

    var options = {
        viewLoader: {
            load: function (path) {
                pathLoaded = path;

                return {
                    continueWith: function (callback) {
                        callback(html);
                    }
                };
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
    view.load("somePath").continueWith(function (actualView) {
        continuedWithView = actualView;
    });

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

    it("should continue when the view loader is continued", function () {
        expect(continuedWithView).toBe(view);
    });
});