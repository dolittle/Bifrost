describe("when loading and applying all viewmodels with two viewmodels", function () {

    var firstElement = $("<div data-viewmodel-file='somePath/first/viewModel.js'/>");
    var secondElement = $("<div data-viewmodel-file='somePath/second/viewModel.js'/>");

    var firstViewModel = { something: 42, activated: sinon.stub() };
    var secondViewModel = { something: 43, activated: sinon.stub() };

    var appliedBindings = [];

    var firstPromise = {
        continueWith: function (callback) {
            firstPromise.callback = callback;
        }
    };

    var secondPromise = {
        continueWith: function (callback) {
            secondPromise.callback = callback;
        }
    };

    var viewModelApplied = null;

    beforeEach(function () {

        var manager = Bifrost.views.viewModelManager.createWithoutScope({
            assetsManager: {},
            documentService: {
                getAllElementsWithViewModelFilesSorted: function () {
                    return [secondElement, firstElement];
                },
                getViewModelFileFrom: function (element) {
                    if (element == firstElement) return "somePath/first/viewModel.js";
                    return "somePath/second/viewModel.js";
                },
                setViewModelOn: function (element, viewModel) {
                    element.viewModel = viewModel;
                },
                getViewModelFrom: function (element) {
                    return element.viewModel;
                },
            },
            viewModelLoader: {
                load: function (viewModel) {
                    if (viewModel == "somePath/first/viewModel.js") return firstPromise;
                    return secondPromise;
                }
            }
        });

        sinon.stub(ko, "applyBindings", function (viewModel) {
            viewModelApplied = viewModel;
        });

        manager.loadAndApplyAllViewModelsInDocument();

        firstPromise.callback(firstViewModel);
        secondPromise.callback(secondViewModel);
    });

    afterEach(function () {
        ko.applyBindings.restore();
    });

    it("should apply a master view model", function() {
        expect(ko.applyBindings.called).toBe(true);
    });

    it("should have an observable property for first viewmodel", function() {
        expect(ko.isObservable(viewModelApplied.somePathfirstviewModel)).toBe(true);
    });

    it("should have an observable property for second viewmodel", function() {
        expect(ko.isObservable(viewModelApplied.somePathsecondviewModel)).toBe(true);
    });

    it("should apply set binding expression to point to first viewmodel for its element", function () {
        expect($(firstElement).attr("data-bind").indexOf("viewModel: $data.somePathfirstviewModel") >= 0).toBe(true);
    });

    it("should apply set binding expression to point to second viewmodel for its element", function () {
        expect($(secondElement).attr("data-bind").indexOf("viewModel: $data.somePathsecondviewModel") >= 0).toBe(true);
    });

    it("should activate first viewmodel", function () {
        expect(firstViewModel.activated.called).toBe(true);
    });

    it("should activate second viewmodel", function () {
        expect(secondViewModel.activated.called).toBe(true);
    });
});