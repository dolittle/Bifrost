describe("when creating without message", function () {
    var rule = Bifrost.validation.Rule.create({ options: { } });

    it("should set empty message in rule", function () {
        expect(rule.message).toBe("");
    });
});