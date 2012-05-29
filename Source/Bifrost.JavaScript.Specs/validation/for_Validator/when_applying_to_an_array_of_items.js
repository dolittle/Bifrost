describe("when applying to an array of items", function () {

    var items,
        validatorCount;

    beforeEach(function () {
        items = [{}, {}, {}];
        Bifrost.validation.Validator.applyTo(items, {});
        validatorCount = 0;
        items.forEach(function (item) {
            if (typeof item.validator !== "undefined") {
                validatorCount++;
            }
        });

    });

    it("should add a validator to all items", function () {
        expect(validatorCount).toBe(items.length);
    });
});