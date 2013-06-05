describe("when initial value is not set with value on observable", function () {
    var target = ko.observable(42);
    ko.extenders.hasChanges(target, {});

    it("should not have changes", function () {
        expect(target.hasChanges()).toBe(false);
    });
});