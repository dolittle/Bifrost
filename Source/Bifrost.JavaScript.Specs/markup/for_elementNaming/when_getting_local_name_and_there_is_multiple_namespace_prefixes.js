describe("when getting local name and there is multiple namespace prefixes", given("an element naming instance", function () {
    var context = this;
    var exception = null;

    beforeEach(function () {
        var element = { localName: "ns:ns2:something", attributes: [], isKnownType: sinon.stub().returns(false) };

        (function becauseOf() {
            try {
                context.elementNaming.getLocalNameFor(element);
            } catch (e) {
                exception = e;
            }
        })();
    });

    it("should throw multiple namespaces in name not allowed", function () {
        expect(exception instanceof Bifrost.markup.MultipleNamespacesInNameNotAllowed).toBe(true);
    });
}));