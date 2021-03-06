﻿describe("when asking if map exists for source and target and source is not a type", function () {

    var mapType = null;
    var result = null;

    beforeEach(function () {
        mapType = Bifrost.mapping.Map;
        Bifrost.mapping.Map = Bifrost.Type.extend(function () { });

        var maps = Bifrost.mapping.maps.createWithoutScope();
        var sourceType = Bifrost.Type.extend(function () { });
        var targetType = null;

        result = maps.hasMapFor(sourceType, targetType);
    });

    afterEach(function () {
        Bifrost.mapping.Map = mapType;
    });

    it("should not have map", function () {
        expect(result).toBe(false);
    });
});
