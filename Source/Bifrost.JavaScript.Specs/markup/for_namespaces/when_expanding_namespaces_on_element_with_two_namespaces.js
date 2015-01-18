describe("when expanding namespaces on element with two namespaces", given("a namespaces instance", function () {
    var context = this;

    var firstPrefix = "first";
    var firstTarget = "firstTarget";
    var secondPrefix = "second";
    var secondTarget = "secondTarget";

    var element;


    beforeEach(function () {
        element = document.createElement("div");
        var firstPrefixAttribute = document.createAttribute("ns:"+firstPrefix);
        firstPrefixAttribute.value = firstTarget;
        element.attributes.setNamedItem(firstPrefixAttribute);

        var secondPrefixAttribute = document.createAttribute("ns:" + secondPrefix);
        secondPrefixAttribute.value = secondTarget;
        element.attributes.setNamedItem(secondPrefixAttribute);

        (function becauseOf() {
            context.namespaces.expandNamespaceDefinitions(element);
        })();
    });

    it("should have two namespace definitions", function () {
        expect(element.__namespaces.length).toBe(2);
    });

    it("should hold the first prefix", function () {
        expect(element.__namespaces[0].prefix).toBe(firstPrefix);
    });
    it("should hold the first prefix target", function () {
        expect(element.__namespaces[0].addTarget.calledWith(firstTarget)).toBe(true);
    });

    it("should hold the second prefix", function () {
        expect(element.__namespaces[1].prefix).toBe(secondPrefix);
    });
    it("should hold the second prefix target", function () {
        expect(element.__namespaces[1].addTarget.calledWith(secondTarget)).toBe(true);
    });
}));