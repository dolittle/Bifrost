describe("when declaring namespace with content", function () {
    Bifrost.namespace("Namespace.With.Content", {
        a_function: function () {
        },
        a_value: 42
    });


    it("should include function from content", function () {
        expect(Namespace.With.Content.a_function).toBeDefined();
    });

    it("should include value from content", function () {
        expect(Namespace.With.Content.a_value).toBe(42);
    });
});