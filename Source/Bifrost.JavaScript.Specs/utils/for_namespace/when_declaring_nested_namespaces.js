describe("when declaring nested namespaces", function () {

    if (window.First) delete window.First;

    Bifrost.namespace("First", {
        something: "Hello"
    });

    Bifrost.namespace("First.Second", {
    });

    it("should point to first in the second part of the namespace", function () {
        expect(First.Second.parent.something).toBe("Hello");
    });
});