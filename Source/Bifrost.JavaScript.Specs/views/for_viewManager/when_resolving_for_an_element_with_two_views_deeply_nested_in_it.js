describe("when resolving an element with two views deeply nested in it", function () {

    var container = $("<div/>");
    var firstViewElement = $("<div data-view='firstView'/>");
    var secondViewElement = $("<div data-view='secondView'/>");
    var thirdElement = $("<div data-nothing='nothing'/>");

    container.append(firstViewElement);
    firstViewElement.append(secondViewElement);
    container.append(thirdElement);
    thirdElement.resolved = false;

    var options = {
        viewResolvers: {
            canResolve: function(element) {
                if (element == firstViewElement[0]) return true;
                if (element == secondViewElement[0]) return true;
                return false;
            },
            resolve: function (element) {
                var name = null;

                if (element == firstViewElement[0]) {
                    firstViewElement.resolved = true;
                    name = "firstView";
                }
                if (element == secondViewElement[0]) {
                    secondViewElement.resolved = true;
                    name = "secondView";
                }
                if (element == thirdElement) thirdElement.resolved = true;

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
                        name : name,
                        element: viewElement,
                        load: function() {
                            return {
                                continueWith:function(callback) {
                                    callback();
                                }
                            }
                        }
                    }
                }

                return null;
            }
        }
    };

    var viewManager = Bifrost.views.viewManager.create(options);
    viewManager.resolve(container[0]);

    it("should resolve first view", function () {
        expect(firstViewElement.resolved).toBe(true);
    });

    it("should resolve second view", function () {
        expect(secondViewElement.resolved).toBe(true);
    });

    it("should not resolve third element that is not a view", function () {
        expect(thirdElement.resolved).toBe(false);
    });

    it("should set the first view on the element", function () {
        expect(container[0].firstChild.view.name).toBe("firstView");
    });

    it("should set the second view on the element", function () {
        expect(container[0].firstChild.firstChild.view.name).toBe("secondView");
    });
});