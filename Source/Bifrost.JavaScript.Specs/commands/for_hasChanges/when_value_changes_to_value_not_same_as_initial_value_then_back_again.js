describe("when value changes to value not same as initial value then back again", function () {
    var changeCount = 0;

    var target = ko.observable(43);
    ko.extenders.hasChanges(target, {});

    target.hasChanges.subscribe(function () {
        changeCount++;
    });
    target.setInitialValue(43);

    target(42);
    target(43);

    it("should not have changes", function () {
        expect(target.hasChanges()).toBe(false);
    });

    it("should change hasChanges twice", function () {
        expect(changeCount).toBe(2);
    });
});