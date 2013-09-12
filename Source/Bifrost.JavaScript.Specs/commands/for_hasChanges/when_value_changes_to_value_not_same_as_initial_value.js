describe("when valuearry changes to value not same as initial", function () {
    var changeCount = 0;

    var array = [0, 1, 2];
    var target = ko.observable(array);
    ko.extenders.hasChanges(target, {});

    target.hasChanges.subscribe(function () {
        changeCount++;
    });

    target.setInitialValue(array);
    array.push(4);
    target(array);

    it("should have changes", function () {
        expect(target.hasChanges()).toBe(true);
    });

    it("should change hasChanges once", function () {
        expect(changeCount).toBe(1);
    });
});