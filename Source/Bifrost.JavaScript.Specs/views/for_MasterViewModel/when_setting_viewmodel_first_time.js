describe("when setting viewmodel first time", function () {

    var viewModel = {
        _type: {
            _namespace: {
                name: "myNamespace"
            },
            _name: "myType"
        },
        activated: sinon.stub()
    };
    var masterViewModel = Bifrost.views.MasterViewModel.create();
    masterViewModel.set(viewModel);

    it("should have a new observable for the viewmodel", function () {
        expect(ko.isObservable(masterViewModel["myNamespace.myType"])).toBe(true);
    });

    it("should initialize the new observable with the viewmodel", function () {
        expect(masterViewModel["myNamespace.myType"]()).toBe(viewModel);
    });

    it("should call the activated function", function () {
        expect(viewModel.activated.called).toBe(true);
    });
});