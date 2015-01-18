describe("when resolving for element with namespace definition set on body", given("a namespaces instance", function () {
    var context = this;
    var prefix = "controls";
    var name = "mycontrol";
    var namespace = "some.namespace";
    var result = null;

    var body = null;
    var parent = null;

    beforeEach(function () {
        body = document.body; 
        body.__namespaces = [
            { prefix: "something", targets: ["some.other.namespace"] },
            { prefix: prefix, targets: [namespace] }
        ];
        parent = document.createElement("div");
        parent.parentElement = body;
        body.appendChild(parent);

        var element = document.createElement(prefix + ":" + name);
        element.parentElement = parent;
        parent.appendChild(element);

        context.elementNaming.prefixToReturn = prefix;

        (function becauseOf() {
            result = context.namespaces.resolveFor(element);
        })();
    });

    it("should resolve to the expected namespace", function () {
        print(result);
        expect(result).toEqual(body.__namespaces[1]);
    });
}));