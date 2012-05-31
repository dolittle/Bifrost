describe("when recursivly extending properties", function () {

    var validatorsList,
        properties;


    beforeEach(function () {
        validatorsList = [];
        properties = {
            prop1: {
                prop1: ko.observable()
            },
            prop2: ko.observable({
                prop1: ko.observable()
            })
        };
        Bifrost.validation.validationService.recursivlyExtendProperties(properties, validatorsList);
    });
    it("should add validation knockout extension", function () {
        var prop1Extended = "validator" in properties.prop1.prop1;
        var prop2Extended = "validator" in properties.prop2().prop1;
        var prop3Extended = "validator" in properties.prop2;

        expect(prop1Extended && prop2Extended && prop3Extended).toBe(true);
    });

    it("should return a list of validators", function () {

        expect(validatorsList.length).toBe(3);
    });
});