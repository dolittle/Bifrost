describe("when extending all properties", function () {

    var properties,
        validatorsList;


    beforeEach(function () {
        
        validatorsList = [];

        properties = {
            prop1: ko.observable(),
            prop2: ko.observable()
        };
        Bifrost.validation.validationService.recursivlyExtendProperties(properties, validatorsList);
    });


    it("should add validation knockout extension", function () {
        var prop1Extended = "validator" in properties.prop1;
        var prop2Extended = "validator" in properties.prop2;

        expect(prop1Extended && prop2Extended).toBe(true);
    });

    it("should return a list of validators", function () {
        expect(validatorsList.length).toBe(2);
    });
});