describe("when creating with message", function () {
    var errorMessage = "This is just wrong";
    var rule = Bifrost.validation.Rule.create({ options: { message: errorMessage } });

    it("should set message in rule", function () {
        expect(rule.message).toBe(errorMessage);
    });
});