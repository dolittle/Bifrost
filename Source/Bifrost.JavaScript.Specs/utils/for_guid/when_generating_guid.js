describe("when generating guid", function () {
    var result = Bifrost.Guid.create();

    it("should return something", function () {
        expect(result).toBeDefined();
    });
    it("should return a valid guid", function () {
        expect(result).toMatch("^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
    });
});