describe("when rendering an element with two views deeply nested in it", function () {

    var container = $("<div/>");
    var firstViewElement = $("<div data-view='firstView'/>");
    var secondViewElement = $("<div data-view='secondView'/>");
    var thirdElement = $("<div data-nothing='nothing'/>");

    container.append(firstViewElement);
    firstViewElement.append(secondViewElement);
    container.append(thirdElement);
    thirdElement.rendered = false;

    var options = {
        viewRenderers: {
            canRender: function(element) {
                if (element == firstViewElement[0]) return true;
                if (element == secondViewElement[0]) return true;
                return false;
            },
            render: function (element) {
                var name = null;

                if (element == firstViewElement[0]) {
                    firstViewElement.rendered = true;
                    name = "firstView";
                }
                if (element == secondViewElement[0]) {
                    secondViewElement.rendered = true;
                    name = "secondView";
                }
                if (element == thirdElement) thirdElement.rendered = true;

                if( name != null ) {
                    var viewElement = document.createElement("div");
                    viewElement.setAttribute(name,"");

                    var children = [];
                    for (var child = element.firstChild; child; child = child.nextSibling) {
                        children.push(child);
                    }

                    for( var i=0; i<children.length; i++ ) {
                        var child = children[i];
                        element.removeChild(child);
                        viewElement.appendChild(child);
                    }

                    return {
                        continueWith: function(callback) {

                            callback({
                                name: name,
                                element: viewElement,
                            });
                        }
                    }
                }

                return null;
            }
        },
        viewModelManager: {
            applyToViewIfAny: sinon.stub()
        }
    };

    var viewManager = Bifrost.views.viewManager.defaultScope().create(options);
    viewManager.render(container[0]);

    it("should render first view", function () {
        expect(firstViewElement.rendered).toBe(true);
    });

    it("should render second view", function () {
        expect(secondViewElement.rendered).toBe(true);
    });

    it("should not render third element that is not a view", function () {
        expect(thirdElement.rendered).toBe(false);
    });

    it("should set the first view on the element", function () {
        expect(container[0].firstChild.view.name).toBe("firstView");
    });

    it("should set the second view on the element", function () {
        expect(container[0].firstChild.firstChild.view.name).toBe("secondView");
    });
});