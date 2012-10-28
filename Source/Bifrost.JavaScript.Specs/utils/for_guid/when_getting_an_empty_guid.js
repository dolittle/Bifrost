describe("when getting an empty guid", function () {
    var emptyGuid = Bifrost.Guid.empty;

    it("should be an ampty guid", function () {
        expect(emptyGuid).toEqual("00000000-0000-0000-0000-000000000000");
    });
});