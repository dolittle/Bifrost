describe("when setting viewmodel second time", function () {

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

    var element = {
    };
    

    var viewModelName = "myNamespace.myType";

    var documentService = {
        getViewModelNameFor: sinon.stub().returns(viewModelName)
    };

    var masterViewModel = Bifrost.views.MasterViewModel.create({ documentService: documentService });
    masterViewModel.setFor(element, firstViewModel);
    masterViewModel.setFor(element, secondViewModel);

    it("should set the second viewmodel as property", function () {
        expect(masterViewModel[viewModelName]).toBe(secondViewModel);
    });

    it("should call the deactivated function on the first instance", function () {
        expect(firstViewModel.deactivated.called).toBe(true);
    });

    it("should call the activated function on the second instance", function () {
        expect(secondViewModel.activated.called).toBe(true);
    });
});