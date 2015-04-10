describe("when getting local name on element with namespace reference", given("an element naming instance", function () {
    var context = this;
    var result = null;

    beforeEach(function () {
        var element = { localName: "ns:something", attributes: [], isKnownType: sinon.stub().returns(false) };

        (function becauseOf() {
            result = context.elementNaming.getLocalNameFor(element);
        })();
    });

    it("should return the correct name", function () {
        expect(result).toBe("something");
    });
}));