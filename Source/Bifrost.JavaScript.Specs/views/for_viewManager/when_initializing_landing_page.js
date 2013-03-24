describe("when initializing landing page", function() {

    var viewManager = Bifrost.views.viewManager.create({
        viewResolvers: {}
    });
    

    var path = Bifrost.path.getFilenameWithoutExtension(document.location.toString());

    var elementResolved = null;
    viewManager.resolve = function (element) {
        elementResolved = element;
    };
    
    viewManager.initializeLandingPage();
    
    it("should set correct view for the body tag", function () {
        expect($("body").data("view")).toBe(path);
    });

    it("should resolve for the body tag", function () {
        expect(elementResolved).toBe($("body")[0]);
    });
});