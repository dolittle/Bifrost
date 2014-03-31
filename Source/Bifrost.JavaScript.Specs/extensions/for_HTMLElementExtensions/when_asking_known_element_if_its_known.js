describe("when asking known element if its known", function () {
    var element = document.createElement("div");
    var isKnown = element.isKnownType();

    it("should be known", function () {
        expect(isKnown).toBe(true);
    });
});
