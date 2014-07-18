describe("when getting map for source and target with one map for the combination", function () {

    var mapType = null;
    var map = null;
    var customMap = null;

    beforeEach(function () {
        mapType = Bifrost.mapping.Map;
        Bifrost.mapping.Map = Bifrost.Type.extend(function () { });
        var sourceType = Bifrost.Type.extend(function () { });
        var targetType = Bifrost.Type.extend(function () { });

        customMap = Bifrost.mapping.Map.extend(function () {
            this.sourceType = sourceType;
            this.targetType = targetType;
        });

        var maps = Bifrost.mapping.maps.createWithoutScope();

        map = maps.getMapFor(sourceType, targetType);
    });
    
    afterEach(function () {
        Bifrost.mapping.Map = mapType;
    });

    it("should get the", function () {
        expect(map._type).toBe(customMap);
    });
});
