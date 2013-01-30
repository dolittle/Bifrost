describe("when expanding for an element with two views in it", function () {

    var container = $("<div/>");
    var firstView = $("<div data-view='firstView'/>");
    var secondView = $("<div data-view='secondView'/>");

    container.append(firstView);
    container.append(secondView);

    Bifrost.views.View.expandFor(container[0]);


});