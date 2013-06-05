describe("when value changes to value not same as initial value", function () {
    var target = ko.observable(43);
    ko.extenders.hasChanges(target, {});
    target.setInitialValue(43);

    target(42);

    it("should have changes", function () {
        expect(target.hasChanges()).toBe(true);
    });
});