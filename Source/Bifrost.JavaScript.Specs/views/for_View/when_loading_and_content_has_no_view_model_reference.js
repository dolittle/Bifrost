describe("when loading and content has no view model reference", function () {
    var html = "<div>some content</div>";
    var pathLoaded = null;

    var viewPathGiven = null;
    var askedForView = false;
    var viewModelApplied = null;
    var targetApplied = null;
    var applyBindingsStub;

    var viewModelInstance = {
    };

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
            get: function (viewModel, viewPath) { },
            hasForView: function () {
                askedForView = true;
                return true;
            },
            getForView: function (viewPath) {
                var promise = Bifrost.execution.Promise.create();
                viewPathGiven = viewPath;
                promise.signal(viewModelInstance);
                return promise;
            }
        },
        viewManager: {
            expandFor: function () { }
        }

    };


    beforeEach(function () {
        applyBindingsStub = sinon.stub(ko, "applyBindings", function (viewModel, target) {
            viewModelApplied = viewModel;
            targetApplied = target;
        });
        var view = Bifrost.views.View.create(options);
        view.load("somePath");
    });

    afterEach(function () {
        applyBindingsStub.restore();
    });

    it("should apply bindings with the view model instance", function () {
        expect(viewModelApplied).toBe(viewModelInstance);
    });

    it("should ask if there is a view model for the view", function () {
        expect(askedForView).toBe(true);
    });
});