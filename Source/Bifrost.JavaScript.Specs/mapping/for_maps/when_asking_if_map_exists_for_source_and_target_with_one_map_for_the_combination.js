describe("when asking if map exists for source and target with one map for the combination", function () {

    var mapType = null;
    var result = null;

    beforeEach(function () {
        mapType = Bifrost.mapping.Map;
        Bifrost.mapping.Map = Bifrost.Type.extend(function () { });
        var sourceType = Bifrost.Type.extend(function () { });
        var targetType = Bifrost.Type.extend(function () { });

        var customMap = Bifrost.mapping.Map.extend(function () {
            this.sourceType = sourceType;
            this.targetType = targetType;
        });

        var maps = Bifrost.mapping.maps.createWithoutScope();

        result = maps.hasMapFor(sourceType, targetType);
    });
    
    afterEach(function () {
        Bifrost.mapping.Map = mapType;
    });

    it("should have map", function () {
        expect(result).toBe(true);
    });
});
