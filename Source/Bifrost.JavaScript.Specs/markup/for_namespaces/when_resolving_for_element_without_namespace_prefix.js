describe("when resolving for element without namespace prefix", given("a namespaces instance", function () {
    var context = this;
    var result;

    beforeEach(function () {
        var element = document.createElement("mycontrol");

        (function becauseOf() {
            result = context.namespaces.resolveFor(element);
        })();
    });

    it("should return global namespace", function () {
        expect(result).toBe(context.namespaces.global);
    });
}));