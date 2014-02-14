describe("when setting viewmodel first time", function () {

    var firstViewModel = {
        _type: {
            _namespace: {
                name: "myNamespace"
            },
            _name: "myType"
        },
        activated: sinon.stub(),
        deactivated: sinon.stub()
    };

    var secondViewModel = {
        _type: {
            _namespace: {
                name: "myNamespace"
            },
            _name: "myType"
        },
        activated: sinon.stub()
    };

    var viewModelName = "myNamespace.myType";
    var masterViewModel = Bifrost.views.MasterViewModel.create({ documentService: {} });
    masterViewModel.set(firstViewModel, viewModelName);
    var firstObservable = masterViewModel[viewModelName];
    masterViewModel.set(secondViewModel, viewModelName);

    it("should reuse the same observable for the second viewmodel", function () {
        expect(masterViewModel[viewModelName]).toBe(firstObservable);
    });

    it("should initialize the new observable with the viewmodel", function () {
        expect(masterViewModel[viewModelName]()).toBe(secondViewModel);
    });

    it("should call the deactivated function on the first instance", function () {
        expect(firstViewModel.deactivated.called).toBe(true);
    });

    it("should call the activated function on the second instance", function () {
        expect(secondViewModel.activated.called).toBe(true);
    });
});