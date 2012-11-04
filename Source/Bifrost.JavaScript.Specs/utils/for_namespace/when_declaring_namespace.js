describe("when declaring namespace", function () {
    Bifrost.namespace("Something.Cool.That.Does.Not.Exist", {
        something: function () {
        },
        objectLiteral: {},
        arrayOfStuff: [],
    });

    it("should introduce namespace", function () {
        expect(Something.Cool.That.Does.Not.Exist).toBeDefined();
    });

    it("should set current namespace on functions inside namespace", function () {
        expect(Something.Cool.That.Does.Not.Exist.something._namespace).toBe(Something.Cool.That.Does.Not.Exist);
    });

    it("should set current namespace on object literals inside namespace", function () {
        expect(Something.Cool.That.Does.Not.Exist.objectLiteral._namespace).toBe(Something.Cool.That.Does.Not.Exist);
    });

    it("should set current namespace on arrays inside namespace", function () {
        expect(Something.Cool.That.Does.Not.Exist.arrayOfStuff._namespace).toBe(Something.Cool.That.Does.Not.Exist);
    });
});