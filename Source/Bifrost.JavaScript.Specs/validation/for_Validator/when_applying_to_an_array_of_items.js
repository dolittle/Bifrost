describe("when applying to an array of items", function () {
    it("should add a validator to all items", function () {
        var items = [{}, {}, {}];
        Bifrost.validation.Validator.applyTo(items, {});
        var validatorCount = 0;
        items.forEach(function (item) {
            if (typeof item.validator !== "undefined") {
                validatorCount++;
            }
        });
        expect(validatorCount).toBe(items.length);
    });
});