describe("when creating instance of a scoped type explicitly ignoring the scope", function () {

    var counter = 0;
    var type = Bifrost.Type.extend(function () {
        var self = this;

        this.id = counter++;
    }).scopeTo(window);

    var firstInstance = type.createWithoutScope();
    var secondInstance = type.createWithoutScope();

    it("should have two different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});