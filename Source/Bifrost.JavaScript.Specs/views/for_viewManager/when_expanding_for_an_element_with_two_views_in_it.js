describe("when expanding for an element with two views in it", function () {

    var container = $("<div/>");
    var firstView = $("<div data-view='firstView'/>");
    var secondView = $("<div data-view='secondView'/>");

    container.append(firstView);
    container.append(secondView);

    var options = {
        viewLocationMapper: {
            resolve: function (view) {
                if (view == "firstView") firstView.resolved = true;
                if (view == "secondView") secondView.resolved = true;

                return view + "Path";
            }
        },
        viewFactory: {
            createFrom: function (viewPath) {
                if (viewPath == "firstViewPath") firstView.created = true;
                if (viewPath == "secondViewPath") secondView.created = true;

                return {
                    path: viewPath
                };
            }
        }
    };

    var viewManager = Bifrost.views.viewManager.create(options);
    viewManager.expandFor(container[0]);

    it("should resolve first view using the view location mapper", function () {
        expect(firstView.resolved).toBe(true);
    });

    it("should resolve second view using the view location mapper", function () {
        expect(secondView.resolved).toBe(true);
    });

    it("should create first view using the view factory", function () {
        expect(firstView.created).toBe(true);
    });

    it("should create second view using the view factory", function () {
        expect(secondView.created).toBe(true);
    });

    it("should set the first view on the element", function () {
        expect(firstView[0].view.path).toBe("firstViewPath");
    });

    it("should set the second view on the element", function () {
        expect(secondView[0].view.path).toBe("secondViewPath");
    });

});