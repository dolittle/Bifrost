describe("when asking query type if has read model and it is not set", function () {
    
    var queryType = Bifrost.read.Query.extend(function () {
    });

    var instance = queryType.create();
    var result = instance.hasReadModel();

    it("should return false", function () {
        expect(result).toBe(false);
    });

});