describe("when asking if number is object", function () {

    var result = Bifrost.isObject(5);

    it("should not be considered an object", function () {
        expect(result).toBe(false);
    });
    
});