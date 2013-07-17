describe("when creating with values", function () {

    var instance = Bifrost.read.PagingInfo.create({
        size: 42,
        number: 43
    });

    it("should set the size", function () {
        expect(instance.size).toBe(42);
    });

    it("should set the number", function () {
        expect(instance.number).toBe(43);
    });
});