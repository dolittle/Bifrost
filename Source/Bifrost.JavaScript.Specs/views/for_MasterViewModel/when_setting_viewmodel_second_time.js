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

    var masterViewModel = Bifrost.views.MasterViewModel.create();
    masterViewModel.set(firstViewModel);
    var firstObservable = masterViewModel["myNamespace.myType"];

    masterViewModel.set(secondViewModel);

    it("should reuse the same observable for the second viewmodel", function () {
        expect(masterViewModel["myNamespace.myType"]).toBe(firstObservable);
    });

    it("should initialize the new observable with the viewmodel", function () {
        expect(masterViewModel["myNamespace.myType"]()).toBe(secondViewModel);
    });

    it("should call the deactivated function on the first instance", function () {
        expect(firstViewModel.deactivated.called).toBe(true);
    });

    it("should call the activated function on the second instance", function () {
        expect(secondViewModel.activated.called).toBe(true);
    });
});