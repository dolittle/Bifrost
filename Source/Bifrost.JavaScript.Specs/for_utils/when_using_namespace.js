describe("when using namespace", function () {
    Bifrost.namespace("Something.Cool.That.Does.Not.Exist");

    it("should introduce namespace", function () {
        expect(Something.Cool.That.Does.Not.Exist).toBeDefined();
    });
});