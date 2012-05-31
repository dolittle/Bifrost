describe("when applying to an item", function () {

    var item;

    beforeEach(function() {
        item = { };
        Bifrost.validation.Validator.applyTo(item, { });
    });
    
    it("should add a validator", function () {
        expect(item.validator).not.toBeUndefined();
    });
});