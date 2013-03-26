describe("when initializing landing page", function() {

    var viewManager = Bifrost.views.viewManager.defaultScope().create({
        viewRenderers: {},
        viewModelManager: {}
    });
    

    var path = Bifrost.path.getFilenameWithoutExtension(document.location.toString());
    var elementRendered = null;

    beforeEach(function() {
        sinon.stub(viewManager,"render", function(element) {
            elementRendered = element;
        });
        viewManager.initializeLandingPage();
    });

    afterEach(function() {
        viewManager.render.restore();
    });

    
    
    it("should set correct view for the body tag", function () {
        expect($("body").data("view")).toBe(path);
    });

    it("should render for the body tag", function () {
        expect(elementRendered).toBe($("body")[0]);
    });
});