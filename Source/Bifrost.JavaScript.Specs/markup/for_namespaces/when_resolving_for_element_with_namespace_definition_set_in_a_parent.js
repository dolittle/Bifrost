describe("when resolving for element with namespace definition set in a parent", given("a namespaces instance", function () {
    var context = this;
    var prefix = "controls";
    var name = "mycontrol";
    var namespace = "some.namespace";
    var result = null;

    var parent = null;

    beforeEach(function () {
        parent = document.createElement("div");
        parent.__namespaces = [
            { prefix: "something", targets: ["some.other.namespace"]},
            { prefix: prefix, targets: [namespace] }
        ];

        var element = document.createElement(prefix + ":" + name);
        element.parentElement = parent;
        parent.appendChild(element);

        context.elementNaming.prefixToReturn = prefix;

        (function becauseOf() {
            result = context.namespaces.resolveFor(element);
        })();
    });

    it("should resolve to the expected namespace", function() {
        expect(result).toEqual(parent.__namespaces[1]);
    });
}));