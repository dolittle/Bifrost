describe("when asking if string is object", function () {

    var result = Bifrost.isObject({});

    it("should be considered an object", function () {
        expect(result).toBe(true);
    });
    
});