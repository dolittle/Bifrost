describe("when inherited map sets source and target", function () {
    var source = "Source";
    var target = "Target";
    
    var map = Bifrost.mapping.Map.extend(function () {
        this.source(source);
        this.target(target);
    });

    var instance = map.create();

    it("should have the correct source type", function () {
        expect(instance.sourceType).toBe(source);
    });
    
    it("should have the correct target type", function () {
        expect(instance.targetType).toBe(target);
    });

});