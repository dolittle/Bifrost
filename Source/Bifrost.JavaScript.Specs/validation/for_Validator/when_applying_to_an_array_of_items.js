describe("when applying to an array of items", function () {

    var items,
        validatorCount;

    beforeEach(function () {
        items = [{}, {}, {}];
        Bifrost.validation.Validator.applyTo(items, {});
        validatorCount = 0;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (typeof item.validator !== "undefined") {
                validatorCount++;
            }
        }
    });

    it("should add a validator to all items", function () {
        expect(validatorCount).toBe(items.length);
    });
});