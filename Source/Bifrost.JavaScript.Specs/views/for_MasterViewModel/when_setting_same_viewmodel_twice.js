describe("when setting same viewmodel twice", function () {

    var viewModel = {
        _type: {
            _namespace: {
                name: "myNamespace"
            },
            _name: "myType"
        },
        activated: sinon.stub(),
        deactivated: sinon.stub()
    };
    var element = {};

    var viewModelName = "someViewModel";
    var documentService = {
        getViewModelNameFor: sinon.stub().returns(viewModelName)
    };

    var masterViewModel = Bifrost.views.MasterViewModel.create({ documentService: documentService });
    masterViewModel.setFor(element, viewModel);
    masterViewModel.setFor(element, viewModel);

    it("should set the viewmodel as a property", function () {
        expect(masterViewModel[viewModelName]).toBe(viewModel);
    });

    it("should call the activated function", function () {
        expect(viewModel.activated.called).toBe(true);
    });

    it("should not call the deactivated function the first time", function () {
        expect(viewModel.deactivated.called).toBe(false);
    });
});