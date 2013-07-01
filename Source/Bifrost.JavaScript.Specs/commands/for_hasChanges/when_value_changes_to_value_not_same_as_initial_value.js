describe("when value changes to value not same as initial value", function () {
    var changeCount = 0;

    var target = ko.observable(43);
    ko.extenders.hasChanges(target, {});

    target.hasChanges.subscribe(function () {
        changeCount++;
    });

    target.setInitialValue(43);

    target(42);

    it("should have changes", function () {
        expect(target.hasChanges()).toBe(true);
    });

    it("should change hasChanges once", function () {
        expect(changeCount).toBe(1);
    });
});