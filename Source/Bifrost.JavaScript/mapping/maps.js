Bifrost.namespace("Bifrost.mapping", {
    maps: Bifrost.Singleton(function() {

        this.canMapSourceProperty = function (property) {
            return false;
        };

        this.mapProperty = function (source, property) {
            return source[property];
        };
    })
});