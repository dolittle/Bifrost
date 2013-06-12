describe("when initial value is set with null on observable differing", function () {
    var target = ko.observable(42);
    ko.extenders.hasChanges(target, {});
    target.setInitialValue(null);

    it("should have changes", function () {
        expect(target.hasChanges()).toBe(true);
    });
});