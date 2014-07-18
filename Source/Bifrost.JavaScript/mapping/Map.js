Bifrost.namespace("Bifrost.mapping", {
    Map: Bifrost.Type.extend(function () {
        var self = this;

        this.sourceType = null;
        this.targetType = null;

        this.source = function (type) {
            self.sourceType = type;
        };

        this.target = function (type) {
            self.targetType = type;
        };

        this.canMapSourceProperty = function (property) {
            return false;
        };

        this.mapProperty = function (source, property) {
            return source[property];
        };
    })
});