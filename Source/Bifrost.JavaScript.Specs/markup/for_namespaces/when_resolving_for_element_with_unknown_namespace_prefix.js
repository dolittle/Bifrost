describe("when resolving for element with unknown namespace prefix", given("a namespaces instance", function () {
    var context = this;

    beforeEach(function () {
        var element = {};

        (function becauseOf() {
            context.namespaces.resolveFor(element);
        })();
    });
}));