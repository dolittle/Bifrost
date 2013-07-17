describe("when asking query type if has read model and it is set", function () {
    
    var queryType = Bifrost.read.Query.extend(function () {
        this.readModel = "Something";
    });

    var instance = queryType.create();
    var result = instance.hasReadModel();

    it("should return true", function () {
        expect(result).toBe(true);
    });

});