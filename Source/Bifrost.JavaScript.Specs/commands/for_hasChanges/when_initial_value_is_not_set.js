describe("when initial value is not set", function () {
    var target = ko.observable();
    ko.extenders.hasChanges(target, {});

    it("should not have changes", function () {
        expect(target.hasChanges()).toBe(false);
    });
});