describe("when expanding namespaces on element with namespace of same prefix as a parent definition", given("a namespaces instance", function () {
    var context = this;

    var prefix = "prefix";
    var firstTarget = "firstTarget";
    var secondTarget = "secondTarget";

    var parent;
    var element;

    beforeEach(function () {
        parent = document.createElement("div");
        var firstPrefixAttribute = document.createAttribute("ns:" + prefix);
        firstPrefixAttribute.value = firstTarget;
        parent.attributes.setNamedItem(firstPrefixAttribute);

        element = document.createElement("div");
        var secondPrefixAttribute = document.createAttribute("ns:" + prefix);
        secondPrefixAttribute.value = secondTarget;
        element.attributes.setNamedItem(secondPrefixAttribute);
        element.parentElement = parent;

        parent.appendChild(element);

        (function becauseOf() {
            context.namespaces.expandNamespaceDefinitions(parent);
            context.namespaces.expandNamespaceDefinitions(element);
        })();
    });

    it("should have one namespace definition on parent", function () {
        expect(parent.__namespaces.length).toBe(1);
    });

    it("should add the first target to the definition", function () {
        expect(parent.__namespaces[0].addTarget.calledWith(firstTarget)).toBe(true);
    });

    it("should add the second target to the definition", function () {
        expect(parent.__namespaces[0].addTarget.calledWith(secondTarget)).toBe(true);
    });
}));