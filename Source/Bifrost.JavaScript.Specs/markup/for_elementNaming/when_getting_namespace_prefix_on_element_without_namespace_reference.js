describe("when getting namespace prefix on element without namespace reference", given("an element naming instance", function () {
    var context = this;
    var result = null;

    beforeEach(function () {
        var element = { localName: "something", attributes: [], isKnownType: sinon.stub().returns(false) };

        (function becauseOf() {
            result = context.elementNaming.getNamespacePrefixFor(element);
        })();
    });

    it("should return empty namespace", function () {
        expect(result).toBe("");
    });
}));