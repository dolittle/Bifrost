describe("when initial value is set with value on observable differing", function () {
    var target = ko.observable(42);
    ko.extenders.hasChanges(target, {});
    target.setInitialValue(43);

    it("should have changes", function () {
        expect(target.hasChanges()).toBe(true);
    });
});