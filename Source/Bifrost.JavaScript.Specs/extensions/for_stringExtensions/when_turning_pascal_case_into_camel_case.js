describe("when turning pascal case into camel case", function () {

    var result = "Pascal-Case".toCamelCase();

    it("should convert as expected", function () {
        expect(result).toBe("pascalCase");
    });
});