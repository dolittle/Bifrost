Bifrost.namespace("Bifrost.mapping", {
    maps: Bifrost.Singleton(function () {
        var self = this;
        var maps = {};

        function getKeyFrom(sourceType, targetType) {
            return sourceType._typeId + " - " + targetType._typeId;
        }

        var extenders = Bifrost.mapping.Map.getExtenders();

        extenders.forEach(function (extender) {
            var map = extender.create();
            var key = getKeyFrom(map.sourceType, map.targetType);
            maps[key] = map;
        });

        this.hasMapFor = function (sourceType, targetType) {
            if (Bifrost.isNullOrUndefined(sourceType) || Bifrost.isNullOrUndefined(targetType)) return false;
            var key = getKeyFrom(sourceType, targetType);
            return maps.hasOwnProperty(key);
        };

        this.getMapFor = function (sourceType, targetType) {
            if (self.hasMapFor(sourceType, targetType)) {
                var key = getKeyFrom(sourceType, targetType);
                return maps[key];
            }
        };
    })
});