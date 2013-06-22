describe("when turning pascal case into camel case", function () {

    var result = "camel-Case".toPascalCase();

    it("should convert as expected", function () {
        expect(result).toBe("CamelCase");
    });
});