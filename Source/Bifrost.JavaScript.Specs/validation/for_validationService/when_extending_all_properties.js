describe("when extending all properties", function () {
    function something() {
        var self = this;

        this.extend = function (extension) {
            self.extendedWith = extension;
        }
    }

    var containingObject = {
        prop1: new something(),
        prop2: new something()
    }
    Bifrost.validation.validationService.extendAllProperties(containingObject);

    it("should add validation knockout extension", function () {
        var prop1Extended = typeof containingObject.prop1.extendedWith.validation !== "undefined";
        var prop2Extended = typeof containingObject.prop2.extendedWith.validation !== "undefined";

        expect(prop1Extended && prop2Extended).toBe(true);
    });
});