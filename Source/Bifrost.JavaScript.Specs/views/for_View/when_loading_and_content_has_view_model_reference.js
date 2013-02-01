describe("when loading and content has view model reference", function () {
    var html = "<div data-viewmodel='someViewModel'>some content</div>";
    var pathLoaded = null;

    var viewModelName = null;
    var viewPathGiven = null;
    var viewModelApplied = null;
    var targetApplied = null;

    var applyBindingsStub;

    var viewModelInstance = {
    };

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
            get: function (viewModel, viewPath) {
                var promise = Bifrost.execution.Promise.create();
                viewModelName = viewModel;
                viewPathGiven = viewPath;
                promise.signal(viewModelInstance);
                return promise;
            }
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

    it("should ask view model manager for the view model", function () {
        expect(viewModelName).toBe("someViewModel");
    });

    it("should apply bindings with the view model instance", function () {
        expect(viewModelApplied).toBe(viewModelInstance);
    });

    it("should apply view model to correct target", function () {
        expect($(targetApplied).html()).toEqual($(html).html());
    });
});