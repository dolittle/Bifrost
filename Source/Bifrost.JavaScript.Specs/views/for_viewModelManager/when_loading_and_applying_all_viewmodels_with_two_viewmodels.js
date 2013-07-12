describe("when loading and applying all viewmodels with two viewmodels", function () {

    var firstElement = $("<div data-viewmodel-file='first'/>");
    var secondElement = $("<div data-viewmodel-file='second'/>");

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



    beforeEach(function () {

        var manager = Bifrost.views.viewModelManager.createWithoutScope({
            assetsManager: {},
            documentService: {
                getAllElementsWithViewModelFilesSorted: function () {
                    return [secondElement, firstElement];
                },
                getViewModelFileFrom: function (element) {
                    if (element == firstElement) return "first";
                    return "second";
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
                    if( viewModel == "first" ) return firstPromise;
                    return secondPromise;
                }
            }
        });

        sinon.stub(ko, "applyBindingsToNode", function (target, bindings) {
            appliedBindings.push({ element: target, bindings: bindings });
        });

        manager.loadAndApplyAllViewModelsInDocument();

        firstPromise.callback(firstViewModel);
        secondPromise.callback(secondViewModel);
    });

    afterEach(function () {
        ko.applyBindingsToNode.restore();
    });

    it("should apply two viewmodels", function() {
        expect(appliedBindings.length).toBe(2);
    });

    it("should apply second viewmodel first", function () {
        expect(appliedBindings[0].bindings.with).toBe(secondViewModel);
    });

    it("should apply first viewmodel second", function () {
        expect(appliedBindings[1].bindings.with).toBe(firstViewModel);
    });

    it("should activate first viewmodel", function () {
        expect(firstViewModel.activated.called).toBe(true);
    });

    it("should activate second viewmodel", function () {
        expect(secondViewModel.activated.called).toBe(true);
    });
});