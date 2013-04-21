describe("when replacing many instances of a char with new char", function () {

    result = "something_with_many_underscores".replaceAll("_", " ");

    it("should replace all", function () {
        expect(result).toBe("something with many underscores");
    });
});