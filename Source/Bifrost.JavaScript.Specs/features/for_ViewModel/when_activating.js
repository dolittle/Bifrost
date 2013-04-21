describe("when activating", function () {
    var instance = null;
    var handleUriState = false;

    function MyViewModel() {
    }

    beforeEach(function () {
        sinon.stub(Bifrost.Uri, "create", function () {
            return {
                setLocation: function () { }
            }
        });

        Bifrost.messaging = Bifrost.messaging || {}
        Bifrost.messaging.Messenger = {
            global: {}
        };


        Bifrost.features.ViewModel.baseFor(MyViewModel);
        sinon.stub(MyViewModel.prototype, "handleUriState", function () {
            handleUriState = true;
        });

        instance = new MyViewModel();
        instance.onActivated();
    });

    afterEach(function () {
        Bifrost.Uri.create.restore();
        MyViewModel.prototype.handleUriState.restore();
    });


    it("should handle uri state", function () {
        expect(handleUriState).toBe(true);
    });
});