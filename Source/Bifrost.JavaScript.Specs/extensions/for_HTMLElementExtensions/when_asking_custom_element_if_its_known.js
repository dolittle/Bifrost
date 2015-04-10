describe("when asking custom element if its known", function () {
    var element = document.createElement("custom.element");
    element.constructor = new HTMLUnknownElement();
    var isKnown = element.isKnownType();

    it("should not be known", function () {
        expect(isKnown).toBe(false);
    });
});
