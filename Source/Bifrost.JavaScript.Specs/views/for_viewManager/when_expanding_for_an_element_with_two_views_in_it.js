describe("when expanding for an element with two views in it", function () {

    var container = $("<div/>");
    var firstViewElement = $("<div data-view='firstView'/>");
    var secondViewElement = $("<div data-view='secondView'/>");

    container.append(firstViewElement);
    container.append(secondViewElement);

    var options = {
        viewLocationMapper: {
            resolve: function (view) {
                if (view == "firstView") firstViewElement.resolved = true;
                if (view == "secondView") secondViewElement.resolved = true;

                return view + "Path";
            }
        },
        viewFactory: {
            createFrom: function (viewPath) {
                var promise = Bifrost.execution.Promise.create();


                if (viewPath == "firstViewPath") firstViewElement.created = true;
                if (viewPath == "secondViewPath") secondViewElement.created = true;

                var view = {
                    path: viewPath,
                    content: $("<div>" + viewPath + "</div>")[0].outerHTML
                };
                promise.signal(view);

                return promise;
            }
        }
    };

    var viewManager = Bifrost.views.viewManager.create(options);
    viewManager.expandFor(container[0]);

    it("should resolve first view using the view location mapper", function () {
        expect(firstViewElement.resolved).toBe(true);
    });

    it("should resolve second view using the view location mapper", function () {
        expect(secondViewElement.resolved).toBe(true);
    });

    it("should create first view using the view factory", function () {
        expect(firstViewElement.created).toBe(true);
    });

    it("should create second view using the view factory", function () {
        expect(secondViewElement.created).toBe(true);
    });

    it("should set the first view on the element", function () {
        expect(firstViewElement[0].view.path).toBe("firstViewPath");
    });

    it("should set the second view on the element", function () {
        expect(secondViewElement[0].view.path).toBe("secondViewPath");
    });

    it("should append the content to the first view", function () {
        expect(firstViewElement.html()).toEqual(firstViewElement[0].view.content);
    });
});