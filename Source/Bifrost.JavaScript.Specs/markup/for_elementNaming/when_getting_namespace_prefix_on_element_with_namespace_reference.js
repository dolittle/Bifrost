describe("when getting namespace prefix on element with namespace reference", given("an element naming instance", function () {
    var context = this;
    var result = null;

    beforeEach(function () {
        var element = { localName: "ns:something", attributes: [], isKnownType: sinon.stub().returns(false) };

        (function becauseOf() {
            result = context.elementNaming.getNamespacePrefixFor(element);
        })();
    });

    it("should return the correct prefix", function () {
        expect(result).toBe("ns");
    });
}));